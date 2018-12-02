import 'dart:convert';
import 'dart:io';
import 'package:http/http.dart';
import 'package:path/path.dart';
import 'package:path_provider/path_provider.dart';

import 'package:virtual_librarian/data/book_downloaded/downloaded_book.dart';

class BookDownloadedRepositoryData implements BookDownloadedRepository {
  @override
  Future deleteBook(int id) async {
    var dir = await getApplicationDocumentsDirectory();
    var path = "${dir.path}/pdf/$id.pdf";

    if (FileSystemEntity.typeSync(path) != FileSystemEntityType.notFound) {
      File(path).delete();
    } else {
      throw new DeleteBookException("ERROR: File deletion failure.");
    }
  }

  @override
  Future <List<DownloadedBook>> fetchDownloadedBooks() async {
    final url = "http://192.168.43.167:50863/book";   //TODO: Edit to match

    var client = new Client();
    List<String> pdfList = await getDownloadedIdList();
    
    List<Response> bookList = await Future.wait(pdfList.map((bookId) => client.get("$url/$bookId")));
    return bookList.map((response){
      return new DownloadedBook.fromJson(json.decode(response.body));
    }).toList();
  }

  @override
  Future openBook(int id) async {
    var dir = await getApplicationDocumentsDirectory();
    var path = "${dir.path}/pdf/$id.pdf";
    if (FileSystemEntity.typeSync(path) != FileSystemEntityType.notFound) {
      return path;
    } else {
      throw new DeleteBookException("ERROR: File was not found.");
    }
  }
  
  Future <List<String>> getDownloadedIdList() async {
    var dir = await getApplicationDocumentsDirectory();
    var pdfFolderDir = new Directory("${dir.path}/pdf");
    if (!pdfFolderDir.existsSync()) {
      pdfFolderDir.create(recursive: false);
    }
    var _filesPDF = pdfFolderDir.listSync(recursive: false, followLinks: false);
    var filesList = _filesPDF.toList();
    List<String> pdfList = new List();
    filesList.forEach((file){
      String filename = basename(file.path);
      pdfList.add(filename.replaceAll(".pdf", ""));
    });
    
    return pdfList;
  }
}