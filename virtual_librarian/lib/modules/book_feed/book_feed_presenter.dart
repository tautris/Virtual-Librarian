import 'package:virtual_librarian/data/dependency_injection.dart';
import 'package:virtual_librarian/data/book_feed/feed_book.dart';

abstract class BookFeedListViewContract {
  void onLoadFeedComplete(List<FeedBook> items);
  void onLoadFeedError();
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
}
