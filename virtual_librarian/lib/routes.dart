import 'package:flutter/material.dart';
import 'package:virtual_librarian/views/home_screen.dart';
import 'package:virtual_librarian/views/login_view.dart';

  final routes = <String, WidgetBuilder> {
     HomeScreenState.tag: (context) => HomeScreen(),
     LoginPage.tag: (context) => LoginPage(),
  };