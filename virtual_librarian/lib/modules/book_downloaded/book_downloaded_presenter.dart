import 'dart:io';

import 'package:virtual_librarian/data/book_downloaded/downloaded_book.dart';
import 'package:virtual_librarian/data/dependency_injection.dart';

abstract class DownloadedBookListViewContract {
  void onLoadDownloadedComplete(List<DownloadedBook> items);
  void onLoadDownloadedError();
  void onRetrieveFilePathComplete(File path);
  void openBookError();
  void deleteBookError();
}

class BookDownloadedListPresenter {
  DownloadedBookListViewContract _view;
  BookDownloadedRepository _repository;

  BookDownloadedListPresenter(this._view) {
    _repository = new Injector().bookDownloadedRepository;
  }

  void loadBooks() {
  _repository
      .fetchDownloadedBooks()
      .then((book) => _view.onLoadDownloadedComplete((book)))
      .catchError((onError) => _view.onLoadDownloadedError());
  }

  void openBook(int id) {
    _repository
      .openBook(id)
      .then((path) => _view.onRetrieveFilePathComplete(path))
      .catchError((onError) => _view.openBookError());
  }

  void deleteBook(int id) {
    _repository
      .deleteBook(id)
      .catchError((onError) => _view.deleteBookError());
  }
}
