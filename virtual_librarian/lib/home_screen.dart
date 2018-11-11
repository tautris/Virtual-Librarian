import 'package:flutter/material.dart';

import 'package:virtual_librarian/tabs/home/view.dart';
import 'package:virtual_librarian/tabs/books/view.dart';
import 'package:virtual_librarian/tabs/statistics/view.dart';
import 'package:virtual_librarian/tabs/profile/view.dart';

class HomePage extends StatefulWidget {
  static String tag = 'home-page';
  @override
  State<StatefulWidget> createState() => new HomeScreenState();
}

class HomeScreenState extends State<HomePage> with SingleTickerProviderStateMixin {
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
      // appBar: new AppBar(
      //   title: new Text("Virtual Librarian"),
      //   backgroundColor: Colors.green,
      // ),
      body: new TabBarView(
        children: <Widget>[new RefreshState(), new BookTab(), new StatisticsTab(), new ProfileTab()],
        controller: controller,
      ),
      bottomNavigationBar: new Material(
        color: Colors.white,
        child: new TabBar(
          labelColor: Colors.black,
          indicatorColor: Colors.black,
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