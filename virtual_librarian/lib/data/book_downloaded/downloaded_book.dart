class DownloadedBook {
  final int id;
  final String author;
  final String title;
  final String imageURL;

  DownloadedBook(this.id, this.author, this.title, this.imageURL);

  DownloadedBook.fromJson(Map<String, dynamic> json)
    : id = json['id'],
      author = json['author'],
      title = json['title'],
      imageURL = json['image'];
}

abstract class BookDownloadedRepository {
  Future <List<DownloadedBook>> fetchDownloadedBooks();
  Future openBook(int id);
  Future deleteBook(int id);
}

class FetchDataException implements Exception {
  final _message;

  FetchDataException([this._message]);

  String toString() {
    if (_message == null) return "Exception";
    return "Exception: $_message";
  }
}

class OpenBookException implements Exception {
  final _message;

  OpenBookException([this._message]);

  String toString() {
    if (_message == null) return "Exception";
    return "Exception: $_message";
  }
}

class DeleteBookException implements Exception {
  final _message;

  DeleteBookException([this._message]);

  String toString() {
    if (_message == null) return "Exception";
    return "Exception: $_message";
  }
}