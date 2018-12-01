class FeedBook {
  int id;
  String title;
  String author;
  int likes;
  String description;
  String imageURL;
  String pdfURL;

  FeedBook(this.id, this.title, this.author, this.likes, this.description, this.imageURL, this.pdfURL);

  FeedBook.fromMap(Map<String, dynamic> map)
    : id = map['id'],
      title = map['title'],
      author = map['author'],
      likes = map['likes'],
      description = map['description'],
      imageURL = map['imageURL'],
      pdfURL = map['pdfURL'];
}

abstract class BookFeedRepository {
  Future<List<FeedBook>> fetchBooks();
}

class FetchDataException implements Exception {
  final _message;

  FetchDataException([this._message]);

  String toString() {
    if (_message == null) return "Exception";
    return "Exception: $_message";
  }
}