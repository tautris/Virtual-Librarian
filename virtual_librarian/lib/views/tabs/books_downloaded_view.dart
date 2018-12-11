import 'dart:io';
import 'package:flutter/material.dart';
import 'package:open_file/open_file.dart';

import 'package:virtual_librarian/data/book_downloaded/downloaded_book.dart';
import 'package:virtual_librarian/modules/book_downloaded/book_downloaded_presenter.dart';

class MyBooks extends StatefulWidget {
  @override
  State<StatefulWidget> createState() {
    return _MyBooksState(); 
  }
}

class _MyBooksState extends State<MyBooks> implements DownloadedBookListViewContract{
  BookDownloadedListPresenter _presenter;

  final GlobalKey<RefreshIndicatorState> _refreshIndicatorKey = new GlobalKey<RefreshIndicatorState>();
  bool _isLoading = true;
  bool _isRetrievingFile = false;

  List<DownloadedBook> books;
  String filePath;

  _MyBooksState() {
    _presenter = new BookDownloadedListPresenter(this);
  }

  Future _refresh() async {
    _presenter.loadBooks();
  }

  @override
  void initState() {
    super.initState();
    _presenter.loadBooks();
  }

  @override
  void deleteBookError() {
    print("VIEW. Delete Book Error happened.");
  }

  @override
  void onLoadDownloadedComplete(List<DownloadedBook> items) {
    setState(() {
      books = items;
      _isLoading = false;
    });
  }

  @override
  void onRetrieveFilePathComplete(String path) {
    setState(() {
      filePath = path;
      _isRetrievingFile = false;
      OpenFile.open(path);
    });
  }

  @override
  void onLoadDownloadedError() {
    print("VIEW. Load Downloaded Books error happened.");
  }

  @override
  void openBookError() {
    print("VIEW. Open Book error happened.");
  }

  @override
  Widget build(BuildContext context) {
    Widget childView;
    if (_isLoading || _isRetrievingFile) {
      childView = (
        new Center(
          child: CircularProgressIndicator()
        )        
      );
    } else {
      childView = (
        RefreshIndicator(
          key: _refreshIndicatorKey,
          onRefresh: _refresh,
          child: ListView.builder(
            itemCount: this.books != null ? this.books.length : 0,
            itemBuilder: (context, i) {
              final book = this.books[i];
              return new DownloadedBookWidget(
                                        book: book,
                                        deleteBookAction: (){
                                          _presenter.deleteBook(book.id);
                                          setState(() {
                                            _isLoading = true;
                                            _presenter.loadBooks();
                                          });
                                        },
                                        openBookAction: (){
                                          _presenter.openBook(book.id);
                                          setState(() {
                                            _isRetrievingFile = true;
                                          });
                                        });
            }
          )
        )
      );
    }
    return childView;
  }
}

class DownloadedBookWidget extends StatelessWidget {
  final DownloadedBook book;
  final VoidCallback deleteBookAction;
  final VoidCallback openBookAction;

  const DownloadedBookWidget({
    this.book,
    this.deleteBookAction,
    this.openBookAction
  });
  
  @override
  Widget build(BuildContext context) {
    TextTheme textTheme = Theme
      .of(context)
      .textTheme;
    return new GestureDetector (
      onTap: openBookAction,
      child: Container(
        margin: const EdgeInsets.symmetric(horizontal: 10.0, vertical: 5.0),
        padding: const EdgeInsets.symmetric(horizontal: 15.0, vertical: 10.0),
        decoration: new BoxDecoration(
          color: Colors.grey.shade200.withOpacity(0.3),
          borderRadius: new BorderRadius.circular(5.0),
        ),
        child: new IntrinsicHeight(
          child: new Row(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: <Widget>[
              new Container(
                margin: const EdgeInsets.only(top: 4.0, bottom: 4.0, right: 10.0),
                height: 42.0,
                width: 42.0,
                decoration: new BoxDecoration(
                  shape: BoxShape.circle,
                  image: new DecorationImage(
                    fit: BoxFit.fill,
                    image: NetworkImage(book.imageURL)
                  )
                ),
              ),
              new Expanded(
                child: new Container(
                  child: new Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: <Widget>[
                      new Text(book.title, style: textTheme.subhead),
                      new Text(book.author, style: textTheme.caption),
                    ],
                  ),
                ),
              ),
              new Container(
                margin: new EdgeInsets.symmetric(horizontal: 5.0),
                child: new InkWell(
                  child: new Icon(Icons.delete_sweep, size: 40.0),
                  onTap: deleteBookAction
                ),
              ),
            ],
          ),
        ),
      )
    );
  }
}