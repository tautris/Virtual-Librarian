import 'dart:io';
import 'package:path/path.dart';
import 'package:path_provider/path_provider.dart';

import 'package:virtual_librarian/data/book_downloaded/downloaded_book.dart';

class BookDownloadedRepositoryMock implements BookDownloadedRepository {
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
    List<String> pdfList = await getDownloadedIdList();
    List<DownloadedBook> downloadedBooks = new List();

    pdfList.forEach((id) {
      var idInt = int.parse(id);
      downloadedBooks.add(books[idInt-1]);
    });    
    return downloadedBooks;
  }

  @override
  Future  openBook(int id) async {
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

var books = <DownloadedBook>[
  new DownloadedBook(1, "author1", "book1", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png"),
  new DownloadedBook(2, "author2", "book2", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png"),
  new DownloadedBook(3, "author3", "book3", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png"),
  new DownloadedBook(4, "author4", "book4", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png"),
  new DownloadedBook(5, "author5", "book5", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png"),
  new DownloadedBook(6, "author6", "book6", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png"),
  new DownloadedBook(7, "author7", "book7", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png"),
];