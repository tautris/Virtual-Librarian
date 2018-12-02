import 'package:virtual_librarian/data/book_feed/feed_book.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'dart:async';

class MockBookFeedRepository implements BookFeedRepository {
  @override
  Future<List<FeedBook>> fetchBooks() {
    return new Future.value(books);
  }

  @override
  Future downloadBook(int id, String pdfUrl) {
    // TODO: implement downloadBook
    return null;
  }

  @override
  Future likeBook(int id) {
    // TODO: implement likeBook
    return null;
  }
}

var books = <FeedBook>[
  new FeedBook(1, "Book1", "author1", 1, "Description1", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(1, "Book2", "author2", 2, "Description2", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(1, "Book3", "author3", 3, "Description3", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(1, "Book4", "author4", 4, "Description4", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(1, "Book5", "author5", 5, "Description5", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(1, "Book6", "author6", 6, "Description6", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png", "http://www.africau.edu/images/default/sample.pdf"),
  new FeedBook(1, "Book7", "author7", 7, "Description7", "https://cdn4.iconfinder.com/data/icons/basic-17/80/22_BO_open_book-512.png", "http://www.africau.edu/images/default/sample.pdf")
];