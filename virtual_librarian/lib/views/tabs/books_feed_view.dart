import 'package:flutter/material.dart';
import 'package:folding_cell/folding_cell.dart';

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
           return new BookSummary(
                                  book,
                                  horizontal: true,
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
                                  },
                                  shareBookAction: (){
                                    print("aaa");
                                  },
                                );
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

class BookSummary extends StatelessWidget {
  final FeedBook book;
  final bool horizontal;
  final VoidCallback downloadBookAction;
  final VoidCallback likeBookAction;
  final VoidCallback shareBookAction;

  BookSummary(this.book, {this.horizontal = true, this.downloadBookAction, this.likeBookAction, this.shareBookAction});

  @override
  Widget build(BuildContext context) {

  final bookThumbnail = new Container(
    margin: new EdgeInsets.symmetric(
      vertical: 16.0
    ),
    alignment: horizontal ? FractionalOffset.centerLeft : FractionalOffset.center,
    child: new Container(
      height: 92.0,
      width: 92.0,
      decoration: new BoxDecoration(
        shape: BoxShape.circle,
        image: new DecorationImage(
          fit: BoxFit.fill,
          image: NetworkImage(book.imageURL)
        )
      ),
    ),
  );

  Widget _planetValue({String value, IconData icon}) {
    return new Container(
      child: new Row(
        mainAxisSize: MainAxisSize.min,
        children: <Widget>[
          new Opacity(
            opacity: 0.7,
            child: new Icon(icon),
          ),
          new Container(width: 4.0),
          new Text(value,
           style: TextStyle(
             color: Colors.white54,
             fontSize: 11.0,
           )
          )
        ],
      )
    );
  }

  final bookCardContent = new Container(
    margin: new EdgeInsets.fromLTRB(horizontal ? 76.0 : 16.0, horizontal ? 16.0 : 42.0, 16.0, 16.0),
    constraints: new BoxConstraints.expand(),
    child: new Column(
      crossAxisAlignment: horizontal ? CrossAxisAlignment.start : CrossAxisAlignment.center,
      children: <Widget>[
        new Container(height: 4.0),
        new Text(book.title, style: TextStyle(color: Colors.white)),
        new Container(height: 10.0),
        new Text(book.author, style: TextStyle(color: Colors.white)),
        new Separator(),
        new Row(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            new Expanded(
              flex: horizontal ? 1 : 0,
              child: _planetValue(
                value: "2011",
                icon: Icons.calendar_today,
              ),
            ),
            new Container(
              width: horizontal ? 16.0 : 32.0,
            ),
            new Expanded(
              flex: horizontal ? 1 : 0,
              child: _planetValue(
                value: "485",
                icon: Icons.last_page
              ),
            ),
            new Container(
              width: horizontal ? 20.0 : 40.0,
            ),
            new Expanded(
              flex: horizontal ? 1 : 0,
              child: _planetValue(
                value: book.likes.toString(),
                icon: Icons.thumb_up
              ),
            ),
          ],
        )
      ],
    )
  );

  final bookCard = new Container(
    child: bookCardContent,
    height: horizontal ? 124.0 : 154.0,
    margin: horizontal 
    ? new EdgeInsets.only(left: 46.0)
    : new EdgeInsets.only(top: 72.0),
    decoration: new BoxDecoration(
      color: Colors.grey.shade200.withOpacity(0.3),
      shape: BoxShape.rectangle,
      borderRadius: new BorderRadius.circular(8.0),
      boxShadow: <BoxShadow>[
        new BoxShadow(
          color: Colors.black12,
          blurRadius: 10.0,
          offset: new Offset(0.0, 10.0),
        )
      ]
    ),
  );

  return new GestureDetector(
    onTap: horizontal
    ? () =>  Navigator.of(context).push(
      new PageRouteBuilder(
        pageBuilder: (_, __, ___) => new BookDetailPage(book, downloadBookAction, likeBookAction, shareBookAction),
        transitionsBuilder: (context, animation, secondaryAnimation, child) =>
          new FadeTransition(opacity: animation, child: child),
      ),
    )
    : null,
    child: new Container(
      margin: const EdgeInsets.symmetric(
        vertical: 16.0,
        horizontal: 24.0
      ),
      child: new Stack(
        children: <Widget>[
          bookCard,
          bookThumbnail,
        ],
      ),
    )
  );
  }
}

class Separator extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return new Container(
      margin: new EdgeInsets.symmetric(vertical: 8.0),
      height: 2.0,
      width: 30.0,
      color: new Color(0xFF6BFDD9),
    );
  } 
}

class BookDetailPage extends StatelessWidget {
  final VoidCallback downloadBookAction;
  final VoidCallback likeBookAction;
  final VoidCallback shareBookAction;
  final FeedBook book;

  BookDetailPage(this.book, this.downloadBookAction, this.likeBookAction, this.shareBookAction);

  Container _getBackground() {
    return new Container(
      child: new Image.asset(
        "assets/bookPage.jpeg",
        fit: BoxFit.cover,
        height: 300.0,
      ),
      constraints: new BoxConstraints.expand(height: 300.0),
    );
  }

  Container _getGradient() {
    return new Container(
      margin: new EdgeInsets.only(top: 190.0),
      height: 110.0,
      decoration: new BoxDecoration(
        gradient: new LinearGradient(
          colors: <Color>[
            new Color(0x00404040),
            new Color(0xFF404040)
          ],
          stops: [0.0, 0.9],
          begin: const FractionalOffset(0.0, 0.0),
          end: const FractionalOffset(0.0, 1.0),
        )
      ),
    );
  }

  Widget _getContent() {
    final _overviewTitle = "Overview".toUpperCase();
    return ListView(
      padding: new EdgeInsets.fromLTRB(0.0, 72.0, 0.0, 32.0),
      children: <Widget>[
        new BookSummary(book, horizontal: false),
        new Container(
          padding: new EdgeInsets.symmetric(horizontal: 32.0),
          child: new Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: <Widget>[
              new Text(_overviewTitle, style: TextStyle(color: Colors.white),),
              new Separator(),
              new Text (book.description, style: TextStyle(color: Colors.white),),
            ],
          ),
        )
      ],
    );
  }

  Container _getToolbar (BuildContext context) {
    return new Container(
      margin: new EdgeInsets.only(
        top: MediaQuery
          .of(context)
          .padding
          .top),
        child: new BackButton(color: Color (0xFF6BFDD9)),
      );
  }

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      body: new Container(
        constraints: new BoxConstraints.expand(),
        color: new Color(0xFF404040),
        child: new Stack (
          children: <Widget>[
            _getBackground(),
            _getGradient(),
            _getContent(),
            _getToolbar(context),
          ],
        )
      ),
      floatingActionButton: FloatingActionButton.extended(
      elevation: 4.0,
      icon: const Icon(Icons.file_download),
      label: const Text("Download book"),
      onPressed: downloadBookAction,
    ),
    floatingActionButtonLocation: 
      FloatingActionButtonLocation.centerDocked,
    bottomNavigationBar: BottomAppBar(
      child: new Row(
        mainAxisSize: MainAxisSize.max,
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: <Widget>[
          IconButton(
            icon: Icon(Icons.share),
            onPressed: shareBookAction,
          ),
          IconButton(
            icon: Icon(Icons.thumb_up),
            onPressed: likeBookAction,
          )
        ],
      ),
    ),
    );
  }
}