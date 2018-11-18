import 'package:flutter/material.dart';
import 'package:virtual_librarian/home_screen.dart';
import 'package:virtual_librarian/register_screen.dart';

  final routes = <String, WidgetBuilder> {
     HomeScreenState.tag: (context) => HomeScreen(),
     RegisterPage.tag: (context) => RegisterPage(),
  };