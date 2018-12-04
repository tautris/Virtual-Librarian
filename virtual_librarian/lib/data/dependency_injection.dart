import 'package:virtual_librarian/data/book_downloaded/downloaded_book.dart';
import 'package:virtual_librarian/data/book_downloaded/downloaded_book_data.dart';
import 'package:virtual_librarian/data/book_downloaded/downloaded_book_data_mock.dart';
import 'package:virtual_librarian/data/book_feed/book_feed_data.dart';
import 'package:virtual_librarian/data/book_feed/book_feed_data_mock.dart';
import 'package:virtual_librarian/data/book_feed/feed_book.dart';
import 'package:virtual_librarian/data/login/login.dart';
import 'package:virtual_librarian/data/login/login_data.dart';

enum Flavor {MOCK, PROD}

class Injector {
  static final Injector _singleton = new Injector._internal();
  static Flavor _flavor;

  static void configure (Flavor flavor) {
    _flavor = flavor;
  }

  factory Injector() {
    return _singleton;
  }

  Injector._internal();

  BookFeedRepository get bookFeedRepository{
    switch (_flavor) {
      case Flavor.MOCK: return new BookFeedRepositoryMock();
      default: return new BookFeedRepositoryData();
    }
  }

  BookDownloadedRepository get bookDownloadedRepository{
    switch (_flavor) {
      case Flavor.MOCK: return new BookDownloadedRepositoryMock();
      default: return new BookDownloadedRepositoryData();
    }
  }

  LoginRepository get loginRepository{
    return new LoginRepositoryData();
  }
}