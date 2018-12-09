import 'dart:io';
import 'package:dio/dio.dart';
import 'package:http/http.dart' as http;
import 'package:path/path.dart';
import 'dart:convert';
import 'dart:async';

import 'package:path_provider/path_provider.dart';
import 'package:virtual_librarian/data/book_feed/feed_book.dart';

class BookFeedRepositoryData implements BookFeedRepository {
  String bookFeedUrl = "https://api.myjson.com/bins/xmt8a"; //TODO: Add real URL

  @override
  Future <List<FeedBook>> fetchBooks() async {
    http.Response response = await http.get(bookFeedUrl);
    final List responseBody = json.decode(response.body);
    final statusCode = response.statusCode;
    if (statusCode != 200 || responseBody == null) {
      throw new FetchDataException(
        "ERROR: Data Fetching. Status Code : $statusCode.");
    }

    List<String> downloadedList = await getDownloadedIdList();

    List<FeedBook> bookList = responseBody.map((book){
       var bookObject = new FeedBook.fromMap(book);
       if (downloadedList.contains(bookObject.id.toString())) {
         bookObject.downloaded = true;
       }
    }).toList();

    return bookList;
  }

  @override
  Future openBook(int id) async {
    var dir = await getApplicationDocumentsDirectory();
    var path = "${dir.path}/pdf/$id.pdf";
    if (FileSystemEntity.typeSync(path) != FileSystemEntityType.notFound) {
      return path;
    } else {
      throw new OpenBookException("ERROR: File was not found.");
    }
  }

  @override
  Future <bool> downloadBook(int id, String pdfUrl) async {
    Dio dio = Dio();
    var dir = await getApplicationDocumentsDirectory();
    var fileName = "${id.toString()}.pdf";
    var pdfFileDir = "${dir.path}/pdf/$fileName";
    var pdfFolderDir = new Directory("${dir.path}/pdf");

    if (!pdfFolderDir.existsSync()) {
      pdfFolderDir.create(recursive: false);
    }
    if (FileSystemEntity.typeSync(pdfFileDir) != FileSystemEntityType.notFound) {
      //throw new DownloadBookException("ERROR: Book is already downloaded.");
      return false;
    } else {
      try {
        await dio.download(pdfUrl, pdfFileDir, onProgress: (progress, total) {
          //TODO: Change state to display Progress
          print ("Rec: $progress , Total: $total");
        });
      } catch (e) {
        //throw new DownloadBookException("ERROR: Book download has failed.");
        return false;
      }
      if (FileSystemEntity.typeSync(pdfFileDir) != FileSystemEntityType.notFound) {
        return true;
      } else {
        //throw new DownloadBookException("ERROR: The book was not downloaded succesfully.");
        return false;
      }
    }
  }

  @override
  Future <bool> likeBook(int id) async {
    bool success;
    var url = "http://example.com/likeBook/$id";  //TODO: change URL

    await http.post(url, body: {"name": "User1"})
    .then((response) {
      success = json.decode(response.body)[success];
    });
    return success;
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