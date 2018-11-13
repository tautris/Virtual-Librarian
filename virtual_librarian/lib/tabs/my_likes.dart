import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class MyLikesState extends StatefulWidget {
  @override
  State<StatefulWidget> createState() {
    // TODO: implement createState
    return MyLikesView(); 
  }
}

class MyLikesView extends State<MyLikesState> {
  var _isLoading = true;
  var likedBooks;

  _fetchLikedBookData() async {
    print("Fetching book data");

    final url = "https://api.myjson.com/bins/1aiwbu";
    final response = await http.get(url);
    if (response.statusCode == 200) {
      print(response.body);

      final likedBooksJson = json.decode(response.body);
      likedBooksJson.forEach((likedBook) {
        print(likedBook["title"]);
      });
      
      if (this.mounted) {
        setState(() {
          _isLoading = false;
          this.likedBooks = likedBooksJson;
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
      _fetchLikedBookData();
    } else {
      childView = (
          new LikeFeed()
      );
    }
    return childView;
  }
}

class BookFeed extends StatelessWidget {
  const BookFeed({ this.title, this.author, this.likes });

  final String title;
  final String author;
  final int likes;

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
                backgroundImage: new NetworkImage(
                  'http://thecatapi.com/api/images/get?format=src'
                    '&size=small&type=jpg#${title.hashCode}'
                ),
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
                child: new Icon(Icons.delete_sweep, size: 40.0),
                onTap: () {
                  // TODO(implement)
                  Scaffold.of(context).showSnackBar(new SnackBar(
                    content: new Text("Deleting PDF from local memory (lol not)"),
                  ));
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}

class LikeFeed extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return new ListView(
      children: [
        new BookFeed(title: 'Notes From Underground', author: 'Feodor Dostoyevsky', likes: 4),
        new BookFeed(title: 'Crime and Punishment', author: 'Feodor Dostoyevsky', likes: 13),
        new BookFeed(title: '1984', author: 'George Orwell', likes: 3),
      ],
    );
  }
}