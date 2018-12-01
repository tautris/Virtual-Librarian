import 'package:virtual_librarian/data/book_feed/feed_book.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'dart:async';

class ProdBookFeedRepository implements BookFeedRepository {
  String bookFeedUrl = "https://api.myjson.com/bins/xmt8a"; //TODO: Add real URL
  @override
  Future<List<FeedBook>> fetchBooks() async {
    http.Response response = await http.get(bookFeedUrl);
    final List responseBody = json.decode(response.body);
    final statusCode = response.statusCode;
    if (statusCode != 200 || responseBody == null) {
      throw new FetchDataException(
        "Data Fetching ERROR. Status Code : $statusCode");
    }

    return responseBody.map((book) => new FeedBook.fromMap(book)).toList();
  }
}