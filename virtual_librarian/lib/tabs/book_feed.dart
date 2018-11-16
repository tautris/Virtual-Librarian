import 'dart:io';

import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:dio/dio.dart';
import 'package:path_provider/path_provider.dart';
import 'package:open_file/open_file.dart';
import 'package:virtual_librarian/tabs/my_likes.dart';

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

    final url = "https://api.myjson.com/bins/xmt8a";
    final response = await http.get(url);
    if (response.statusCode == 200) {
      //print(response.body);

      final booksJson = json.decode(response.body);
      booksJson.forEach((book) {
        //print(book["title"]);
      });
      
      if (this.mounted) {
        setState(() {
          _isLoading = false;
          this.books = booksJson;
        });
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    Widget childView;
    if (_isLoading) {
      childView = (
        new Center(
          child: CircularProgressIndicator()//Image.asset('assets/loading.gif')
        )        
      );
      _fetchBookData();
    } else {
     childView = (
       new ListView.builder(
         itemCount: this.books != null ? this.books.length : 0,
         itemBuilder: (context, i) {
           final book = this.books[i];
           return new BookFeed(author: book["author"], likes: book["likes"], title: book["title"], imageURL: book["image"], description: book["description"], pdfURL: "http://www.africau.edu/images/default/sample.pdf",);
         }
       )
     );
    }
    return childView;
  }
}

class BookFeed extends StatelessWidget {
  const BookFeed({ this.title, this.author, this.likes, this.imageURL, this.description, this.pdfURL });

  final String title;
  final String author;
  final int likes;
  final String imageURL;
  final String description;
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
            print("AAA");
            // Navigator.of(context)
            //   .overlay
            //   .insert(OverlayEntry(
            //     builder: (BuildContext context) {
            //      return FunkyOverlay();
            //     }
            //   )
            // );
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
                    var fileName = title.replaceAll(" ", "");
                    try {
                      print("${dir.path}/$fileName.pdf");
                      await dio.download(pdfURL, "${dir.path}/$fileName.pdf", onProgress: (rec, total) {
                        print ("Rec: $rec , Total: $total");
                      });
                    } catch (e) {
                      print (e);
                    }
                    if (File("${dir.path}/$fileName.pdf").existsSync()) {
                      Scaffold.of(context).showSnackBar(new SnackBar(
                        content: new Text("File $title.pdf was successfully downloaded."),
                      ));
                    } else {
                      Scaffold.of(context).showSnackBar(new SnackBar(
                        content: new Text("Something Went Wrong. File was not downloaded."),
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
                    // TODO(implement)
                    Scaffold.of(context).showSnackBar(new SnackBar(
                      content: new Text("You have liked the Book"),
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