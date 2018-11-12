import 'package:flutter/material.dart';
import 'package:virtual_librarian/home_screen.dart';
import 'package:virtual_librarian/login_screen.dart';

  final routes = <String, WidgetBuilder> {
     LoginPage.tag: (context) => LoginPage(),
     HomePage.tag: (context) => HomePage(),
  };