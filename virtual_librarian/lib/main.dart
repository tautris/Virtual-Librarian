import 'package:flutter/material.dart';

import 'package:virtual_librarian/routes.dart';
import 'package:virtual_librarian/login_screen.dart';

void main() => runApp(new LibrarianApp());

class LibrarianApp extends StatelessWidget {

  @override
  Widget build(BuildContext context) {
    //return new LoginScreen();
    return new MaterialApp(
      theme: Theme.of(context).copyWith(
        accentIconTheme: Theme.of(context).accentIconTheme.copyWith(
          color: Colors.black
        ),
        accentColor: Colors.red,
        primaryColor: Colors.white,
        primaryIconTheme: Theme.of(context).primaryIconTheme.copyWith(
          color: Colors.white
        ),
        primaryTextTheme: Theme
            .of(context)
            .primaryTextTheme
            .apply(bodyColor: Colors.black)
      ),
      title: 'Virtual Librarian',
      debugShowCheckedModeBanner: false,
      home: LoginPage(),
      routes: routes,
    ); 
  }
}