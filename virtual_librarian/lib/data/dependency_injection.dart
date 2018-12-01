

import 'package:virtual_librarian/data/book_feed/book_feed_data.dart';
import 'package:virtual_librarian/data/book_feed/book_feed_data_mock.dart';
import 'package:virtual_librarian/data/book_feed/feed_book.dart';

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
      case Flavor.MOCK: return new MockBookFeedRepository();
      default: return new ProdBookFeedRepository();
    }
  }
}