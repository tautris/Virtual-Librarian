import 'package:flutter/material.dart';

class DetailPage extends StatelessWidget {
  final book;

  DetailPage(this.book);

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      appBar: new AppBar(
        iconTheme: IconThemeData(
          color: Colors.black,
        ),
        backgroundColor: Colors.white,
        title: new Text(book["title"]),
      ),
      body: new Center(
        child: new Text(book["description"]),
      ),
    );
  }
}
