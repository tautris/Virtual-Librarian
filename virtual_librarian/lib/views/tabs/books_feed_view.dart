import 'package:flutter/material.dart';

import 'package:virtual_librarian/data/book_feed/feed_book.dart';
import 'package:virtual_librarian/modules/book_feed/book_feed_presenter.dart';

class BookFeed extends StatefulWidget {  
  @override
  State<StatefulWidget> createState() {
    return _BookFeedState();
  }
}

class _BookFeedState extends State<BookFeed> implements BookFeedListViewContract{
  BookFeedListPresenter _presenter;
  
  bool _isLoading = true;
  bool _isDownloading = false;
  var downloadBookAction;

  List<FeedBook>  books;

  _BookFeedState() {
    _presenter = new BookFeedListPresenter(this);
  }

  @override 
  void initState() {
    super.initState();
    _presenter.loadBooks();
  }

  @override
  Widget build(BuildContext context) {
    Widget childView;
    if (_isLoading) {
      childView = (
        new Center(
          child: CircularProgressIndicator()
        )        
      );
    } else {
     childView = (
       new ListView.builder(
         itemCount: this.books != null ? this.books.length : 0,
         itemBuilder: (context, i) {
           final book = this.books[i];
           return new BookFeedWidget(
                                    book: book,
                                    downloadBookAction: (){
                                      setState(() {
                                        _isDownloading = true;
                                      });
                                      _presenter.downloadBookFile(book.id, book.pdfURL);
                                      setState(() {
                                        _isDownloading = false;
                                      });
                                    },
                                    likeBookAction: (){
                                      _presenter.likeBook(book.id);
                                    });
         }
       )
     );
    }
    return childView;
  }

  @override
  void onLoadFeedComplete(List<FeedBook> items) {
    setState(() {
      _isLoading = false;
      books = items;
    });
  }

  @override
  void onLoadFeedError() {
    print("VIEW. Load Feed error happened");
  }

  @override
  void downloadBookError() {
    print("VIEW. Download Book error happened");
  }

  @override 
  void likeBookComplete() {
    setState(() {
      //TODO: Implement Real time Like addition / refresh books?
    });
  }

  @override
  void likebookError() {
    print("VIEW. Like Book error happened");
  }
}

class BookFeedWidget extends StatelessWidget {
  final FeedBook book;
  final VoidCallback downloadBookAction;
  final VoidCallback likeBookAction;

  BookFeedWidget({
    this.book, 
    this.downloadBookAction,
    this.likeBookAction
  });

  @override
  Widget build(BuildContext context) {

    TextTheme textTheme = Theme
      .of(context)
      .textTheme;
    return new Container(
      margin: const EdgeInsets.symmetric(horizontal: 10.0, vertical: 5.0),
      padding: const EdgeInsets.symmetric(horizontal: 15.0, vertical: 10.0),
      decoration: new BoxDecoration(
        color: Colors.grey.shade200.withOpacity(0.3),
        borderRadius: new BorderRadius.circular(5.0),
      ),
      child: new IntrinsicHeight(
        child: new GestureDetector(
          onTap: () {
            print("expand?");
          },
          child: new Row(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: <Widget>[
              new Container(
                margin: const EdgeInsets.only(top: 4.0, bottom: 4.0, right: 10.0),
                child: new CircleAvatar(
                  backgroundImage: new NetworkImage(book.imageURL),
                  radius: 20.0,
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
                  child: new Icon(Icons.backup, size: 40.0),
                  onTap: downloadBookAction
                ),
              ),
              new Container(
                margin: new EdgeInsets.symmetric(horizontal: 5.0),
                child: new InkWell(
                  child: new Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: <Widget>[
                      new Icon(Icons.favorite, size: 25.0),
                      new Text('${book.likes}'),
                    ],
                  ),
                  onTap: likeBookAction
                ),
              ),
            ],
          ),
        )
      ),
    );
  }
}