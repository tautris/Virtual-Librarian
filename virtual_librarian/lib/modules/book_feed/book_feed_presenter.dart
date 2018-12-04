import 'package:virtual_librarian/data/dependency_injection.dart';
import 'package:virtual_librarian/data/book_feed/feed_book.dart';

abstract class BookFeedListViewContract {
  void onLoadFeedComplete(List<FeedBook> items);
  void onLoadFeedError();
  void downloadBookError();
  void likebookError();
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
        .catchError((onError) => _view.downloadBookError());
  }

  void likeBook(int id){
    _repository
      .likeBook(id)
      .catchError((onError) => _view.likebookError());
  }
}
