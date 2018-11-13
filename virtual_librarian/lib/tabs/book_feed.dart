import 'dart:io';

import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

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
      print(response.body);

      final booksJson = json.decode(response.body);
      booksJson.forEach((book) {
        print(book["title"]);
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
           var author = book["author"];
           var likes = book["likes"];
           var title = book["title"];
           return new BookFeed(author: book["author"], likes: book["likes"], title: book["title"], imageURL: book["image"]);
         }
       )
     );
    }
    return childView;
  }
}

class BookFeed extends StatelessWidget {
  const BookFeed({ this.title, this.author, this.likes, this.imageURL });

  final String title;
  final String author;
  final int likes;
  final String imageURL;

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
                onTap: () {
                  // TODO(implement)
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
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}