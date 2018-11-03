import 'package:flutter/material.dart';

import 'package:virtual_librarian/tabs/home/view.dart';
import 'package:virtual_librarian/tabs/books/view.dart';
import 'package:virtual_librarian/tabs/statistics/view.dart';
import 'package:virtual_librarian/tabs/profile/view.dart';

void main() {
  runApp(new MaterialApp(
      title: "Virtual Librarian",
      home: new MyHome()));
}

class MyHome extends StatefulWidget {
  @override
  MyHomeState createState() => new MyHomeState();
}

class MyHomeState extends State<MyHome> with SingleTickerProviderStateMixin {
  TabController controller;

  @override
  void initState() {
    super.initState();

    controller = new TabController(length: 4, vsync: this);
  }

  @override
  void dispose() {

    controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      appBar: new AppBar(
        title: new Text("Virtual Librarian"),
        backgroundColor: Colors.black,
      ),
      body: new TabBarView(
        children: <Widget>[new RefreshState(), new BookTab(), new StatisticsTab(), new ProfileTab()],
        controller: controller,
      ),
      bottomNavigationBar: new Material(
        color: Colors.black,
        child: new TabBar(
          indicatorColor: Colors.white,
          tabs: <Tab>[
            new Tab(
              icon: new Icon(Icons.home),
            ),
            new Tab(
              icon: new Icon(Icons.import_contacts),
            ),
            new Tab(
              icon: new Icon(Icons.airport_shuttle),
            ),
            new Tab(
              icon: new Icon(Icons.account_box),
            )
          ],
          controller: controller,
        ),
      ),
    );
  }
}