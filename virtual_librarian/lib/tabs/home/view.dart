import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

import 'package:virtual_librarian/tabs/home/detail_page.dart';
import 'package:virtual_librarian/tabs/home/book_cell.dart';

class RefreshState extends StatefulWidget {
  @override
  State<StatefulWidget> createState() {
    return new HomeTab();
  }
}

class HomeTab extends State<RefreshState> {
  var _isLoading = true;

  var books;

  _fetchBookData() async {
    print("Fetching book data");

    final url = "https://api.myjson.com/bins/1aiwbu";
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
      _fetchBookData();
        childView = (
          Image.asset('assets/loading.gif')
        );
    } else {
      if (this.books != null) {
        childView = (
          new ListView.builder(
            itemCount: this.books.length,
            itemBuilder: (context, i) {
              final book = this.books[i];
              return new FlatButton(
                padding: new EdgeInsets.all(0.0),
                child: new BookCell(book),
                onPressed: () {
                  print("Book cell tapped: $i");
                  Navigator.push(
                      context,
                      new MaterialPageRoute(
                        builder: (context) => new DetailPage(book),
                      )
                  );
                },
              );
            }
          )
        );
      }
    }
    return new Scaffold(
      backgroundColor: Colors.white,
      body: new Center (
        child: childView,
      )
    );
  }
}