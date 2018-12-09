import 'dart:io';
import 'package:dio/dio.dart';
import 'package:path_provider/path_provider.dart';
import 'dart:async';

import 'package:virtual_librarian/data/book_feed/feed_book.dart';

class BookFeedRepositoryMock implements BookFeedRepository {
  @override
  Future <List<FeedBook>> fetchBooks() {
    return new Future.value(books);
  }

  @override
  Future downloadBook(int id, String pdfUrl) async {
    Dio dio = Dio();
    var dir = await getApplicationDocumentsDirectory();
    var fileName = "${id.toString()}.pdf";
    var pdfFileDir = "${dir.path}/pdf/$fileName";
    var pdfFolderDir = new Directory("${dir.path}/pdf");

    if (!pdfFolderDir.existsSync()) {
      pdfFolderDir.create(recursive: false);
    }
    if (FileSystemEntity.typeSync(pdfFileDir) != FileSystemEntityType.notFound) {
      throw new DownloadBookException("ERROR: Book is already downloaded.");
    } else {
      try {
        await dio.download(pdfUrl, pdfFileDir, onProgress: (progress, total) {
          //TODO: Change state to display Progress
          print ("Rec: $progress , Total: $total");
        });
      } catch (e) {
        throw new DownloadBookException("ERROR: Book download has failed.");
      }
      if (FileSystemEntity.typeSync(pdfFileDir) != FileSystemEntityType.notFound) {
        return;
      } else {
        throw new DownloadBookException("ERROR: The book was not downloaded succesfully.");
      }
    }
  }

  @override
  Future likeBook(int id) {
    // TODO: implement likeBook
    return null;
  }
}

var books = <FeedBook>[
  new FeedBook(1, "Notes From Underground", "Feodor Dostoyevsky", 1, "Long long description. It is long long. Long long description. It is long long. Long long description. It is long long. Long long description. It is long long. ", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(2, "Book2", "author2", 2, "Description2", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(3, "Book3", "author3", 3, "Description3", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(4, "Book4", "author4", 4, "Description4", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(5, "Book5", "author5", 5, "Description5", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(6, "Book6", "author6", 6, "Description6", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(7, "Book7", "author7", 7, "Description7", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png", "http://www.africau.edu/images/default/sample.pdf")
];