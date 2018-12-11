import 'dart:io';
import 'package:dio/dio.dart';
import 'package:path/path.dart';
import 'package:path_provider/path_provider.dart';
import 'dart:async';

import 'package:virtual_librarian/data/book_feed/feed_book.dart';

class BookFeedRepositoryMock implements BookFeedRepository {
  @override
  Future <List<FeedBook>> fetchBooks() async {
    List<String> downloadedList = await getDownloadedIdList();
    List<FeedBook> bookList = books;
    bookList.forEach((book) {
      if (downloadedList.contains(book.id.toString())) {
        book.downloaded = true;
      } else {
        book.downloaded = false;
      }
    });
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
  Future <bool> likeBook(int id) async{
    return true;
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

var books = <FeedBook>[
  new FeedBook(1, "Notes From Underground", "Feodor Dostoyevsky", 1, "Long long description. It is long long. Long long description. It is long long. Long long description. It is long long. Long long description. It is long long. ", "http://www.nationwidebooks.co.nz/application/modules/images/assets/upload/9781843911265.jpg", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(2, "Book2", "author2", 2, "Description2", "https://images-na.ssl-images-amazon.com/images/I/51G9QXQ7QRL.jpg", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(3, "Book3", "author3", 3, "Description3", "https://images-na.ssl-images-amazon.com/images/I/51G9QXQ7QRL.jpg", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(4, "Book4", "author4", 4, "Description4", "https://images-na.ssl-images-amazon.com/images/I/51G9QXQ7QRL.jpg", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(5, "Book5", "author5", 5, "Description5", "https://marketplace.canva.com/MACV2Ehunsw/1/0/thumbnail_large/canva-blue-photo-science-fiction-book-cover-MACV2Ehunsw.jpg", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(6, "Book6", "author6", 6, "Description6", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(7, "Book7", "author7", 7, "Description7", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png", "http://www.africau.edu/images/default/sample.pdf")
];