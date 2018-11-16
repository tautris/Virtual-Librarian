import 'package:camera/camera.dart';
import 'package:flutter/material.dart';

import 'package:virtual_librarian/routes.dart';
import 'package:virtual_librarian/login_screen.dart';
import 'package:flutter/services.dart';

List<CameraDescription> cameras;

Future<Null> main() async {
  SystemChrome.setPreferredOrientations([
    DeviceOrientation.portraitUp,
  ]);
  cameras = await availableCameras();
  runApp(new LibrarianApp());
} 

class LibrarianApp extends StatelessWidget {

  @override
  Widget build(BuildContext context) {
    //return new LoginScreen();
    return new MaterialApp(
      theme: new ThemeData(
        brightness: Brightness.dark,
        primaryColorBrightness: Brightness.dark,
      ),
      title: 'Virtual Librarian',
      debugShowCheckedModeBanner: false,
      home: LoginPage(cameras),
      routes: routes,
    ); 
  }
}