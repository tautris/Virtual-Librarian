import 'package:flutter/material.dart';

class StatisticsTab extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      backgroundColor: Colors.white,
      body: new Container(
        child: new Center(
          child: new Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              new Icon(
                Icons.access_time,
                size: 160.0,
                color: Colors.black,
              ),
              new Text(
                "Statistics Tab",
                style: new TextStyle(color: Colors.black),
              )
            ],
          ),
        ),
      ),
    );
  }
}