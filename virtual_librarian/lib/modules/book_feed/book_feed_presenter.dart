import 'package:virtual_librarian/data/dependency_injection.dart';
import 'package:virtual_librarian/data/book_feed/feed_book.dart';

abstract class BookFeedListViewContract {
  void onLoadFeedComplete(List<FeedBook> items);
  void onLoadFeedError();
  void downloadBookComplete(bool success);
  void downloadBookError();
  void onLikeComplete(bool success);
  void likebookError();
  void onRetrieveFilePathComplete(String path);
  void openBookError();
}

class BookFeedListPresenter {
  BookFeedListViewContract _view;
  BookFeedRepository _repository;

  BookFeedListPresenter(this._view) {
    _repository = new Injector().bookFeedRepository;
  }

  void loadBooks() {
    _repository
        .fetchBooks()
        .then((book) => _view.onLoadFeedComplete((book)))
        .catchError((onError) => _view.onLoadFeedError());
  }

  void downloadBookFile(int id, String pdfUrl){
    _repository
        .downloadBook(id, pdfUrl)
        .then((success) => _view.downloadBookComplete(success))
        .catchError((onError) => _view.downloadBookError());
  }

  void likeBook(int id){
    _repository
      .likeBook(id)
      .then((success) => _view.onLikeComplete(success))
      .catchError((onError) => _view.likebookError());
  }

  void openBook(int id) {
    _repository
      .openBook(id)
      .then((path) => _view.onRetrieveFilePathComplete(path))
      .catchError((onError) => _view.openBookError());
  }
}
