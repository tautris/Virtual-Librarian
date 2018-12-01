import 'dart:io';

import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:dio/dio.dart';
import 'package:path_provider/path_provider.dart';
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
  
  var _isLoading = true;

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
           return new BookFeedModel(book);
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
    print("error happened");
  }
}

class BookFeedModel extends StatelessWidget {
  const BookFeedModel(this.book);

  final FeedBook book;

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
                  onTap: () async {
                    Dio dio = Dio();
                    var dir = await getApplicationDocumentsDirectory();
                    var fileName = book.id.toString();
                    var pdfFileDir = "${dir.path}/pdf/$fileName.pdf";

                    var pdfFolderDir = new Directory("${dir.path}/pdf");
                    if (!pdfFolderDir.existsSync()) {
                      pdfFolderDir.create(recursive: false);
                    }
                    if (FileSystemEntity.typeSync(pdfFileDir) != FileSystemEntityType.notFound) {
                        Scaffold.of(context).showSnackBar(new SnackBar(
                          content: new Text("${book.title} Is already downloaded."),
                          duration: Duration(seconds: 1),
                        ));
                        return;
                    } else {
                      try {
                        print("$pdfFileDir");
                        await dio.download(book.pdfURL, pdfFileDir, onProgress: (rec, total) {
                          print ("Rec: $rec , Total: $total");
                        });
                      } catch (e) {
                        print (e);
                      }
                      if (FileSystemEntity.typeSync(pdfFileDir) != FileSystemEntityType.notFound) {
                        Scaffold.of(context).showSnackBar(new SnackBar(
                          content: new Text("${book.title} was successfully downloaded."),
                          duration: Duration(seconds: 1),
                        ));
                      } else {
                        Scaffold.of(context).showSnackBar(new SnackBar(
                          content: new Text("Something Went Wrong. File was not downloaded."),
                          duration: Duration(seconds: 1),
                        ));
                      }
                    }
                    print("downloaded: ${dir.path}/$fileName.pdf");
                  },
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
                  onTap: () {
                    // TODO(implement post like)
                    Scaffold.of(context).showSnackBar(new SnackBar(
                      content: new Text("You have liked the Book"),
                      duration: Duration(seconds: 1),
                    ));
                  },
                ),
              ),
            ],
          ),
        )
      ),
    );
  }
}