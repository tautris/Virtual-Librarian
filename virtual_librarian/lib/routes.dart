import 'package:flutter/material.dart';
import 'package:virtual_librarian/views/home_screen.dart';
import 'package:virtual_librarian/views/login_view.dart';
import 'package:virtual_librarian/views/register_screen.dart';

  final routes = <String, WidgetBuilder> {
     HomeScreenState.tag: (context) => HomeScreen(),
     RegisterPage.tag: (context) => RegisterPage(),
     LoginPage.tag: (context) => LoginPage(),
  };