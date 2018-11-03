import 'package:flutter/material.dart';

class BookCell extends StatelessWidget {
  final book;

  BookCell(this.book);

  @override
  Widget build(BuildContext context) {
    return new Column(children: <Widget>[
      new Container(
        padding: new EdgeInsets.all(16.0),
        child: new Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            //new Image.network(book["image"]),
            new Image.network(
                "http://ichef.bbci.co.uk/wwfeatures/wm/live/1280_640/images/live/p0/2v/dp/p02vdpfn.jpg"),
            new Container(
              height: 8.0,
            ),
            new Text(
              book["title"],
              style: new TextStyle(fontSize: 16.0, fontWeight: FontWeight.bold),
            ),
            new Text(
              "by " + book["author"],
              style:
                  new TextStyle(fontSize: 14.0, fontWeight: FontWeight.normal),
            ),
          ],
        ),
      ),
      new Divider()
    ]);
  }
}
