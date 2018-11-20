import 'dart:io';

import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:dio/dio.dart';
import 'package:path_provider/path_provider.dart';

class BookFeedState extends StatefulWidget {  
  @override
  State<StatefulWidget> createState() {
    return BookFeed2();
  }
}

class BookFeed2 extends State<BookFeedState> {
  var _isLoading = true;

  var books;

  _fetchBookData() async {
    print("Fetching book data");

    final url = "https://api.myjson.com/bins/exs52";
    final response = await http.get(url);
    if (response.statusCode == 200) {
      //print(response.body);

      final booksJson = json.decode(response.body);
      // booksJson.forEach((book) {
      //   //print(book["title"]);
      // });
      
      if (this.mounted) {
        setState(() {
          _isLoading = false;
          this.books = booksJson;
        });
      }
    }
  }

  _likeBook() async {
    //TODO implement post to like book
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
      _fetchBookData();
    } else {
     childView = (
       new ListView.builder(
         itemCount: this.books != null ? this.books.length : 0,
         itemBuilder: (context, i) {
           final book = this.books[i];
           return new BookFeed(id: book["id"], author: book["author"], likes: book["likes"], title: book["title"], description: book["description"], imageURL: book["image"], pdfURL: "http://www.africau.edu/images/default/sample.pdf");//book["pdf"]);
         }
       )
     );
    }
    return childView;
  }
}

class BookFeed extends StatelessWidget {
  const BookFeed({ this.id, this.title, this.author, this.likes, this.description, this.imageURL, this.pdfURL });

  final int id;
  final String title;
  final String author;
  final int likes;
  final String description;
  final String imageURL;
  final String pdfURL;

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
                  backgroundImage: new NetworkImage(imageURL),
                  radius: 20.0,
                ),
              ),
              new Expanded(
                child: new Container(
                  child: new Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: <Widget>[
                      new Text(title, style: textTheme.subhead),
                      new Text(author, style: textTheme.caption),
                    ],
                  ),
                ),
              ),
              new Container(
                margin: new EdgeInsets.symmetric(horizontal: 5.0),
                child: new InkWell(
                  child: new Icon(Icons.backup, size: 40.0),
                  onTap: () async {
                    //FIXME: check if file exists
                    Dio dio = Dio();
                    var dir = await getApplicationDocumentsDirectory();
                    var fileName = id.toString();
                    var pdfFileDir = "${dir.path}/pdf/$fileName.pdf";

                    var pdfFolderDir = new Directory("${dir.path}/pdf");
                    if (!pdfFolderDir.existsSync()) {
                      pdfFolderDir.create(recursive: false);
                    }

                    try {
                      print("$pdfFileDir");
                      await dio.download(pdfURL, pdfFileDir, onProgress: (rec, total) {
                        print ("Rec: $rec , Total: $total");
                      });
                    } catch (e) {
                      print (e);
                    }
                    if (FileSystemEntity.typeSync(pdfFileDir) != FileSystemEntityType.notFound) {
                      Scaffold.of(context).showSnackBar(new SnackBar(
                        content: new Text("$title was successfully downloaded."),
                        duration: Duration(seconds: 1),
                      ));
                    } else {
                      Scaffold.of(context).showSnackBar(new SnackBar(
                        content: new Text("Something Went Wrong. File was not downloaded."),
                        duration: Duration(seconds: 1),
                      ));
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
                      new Text('${likes ?? ''}'),
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