import 'package:camera/camera.dart';
import 'package:flutter/material.dart';
import 'package:virtual_librarian/data/dependency_injection.dart';

import 'package:virtual_librarian/routes.dart';
import 'package:flutter/services.dart';
import 'package:virtual_librarian/views/login_view.dart';
import 'package:virtual_librarian/views/splash_screen.dart';

//List<CameraDescription> cameras;

Future<Null> main() {
  Injector.configure(Flavor.MOCK);
  SystemChrome.setPreferredOrientations([
    DeviceOrientation.portraitUp,
  ]);
  //cameras = await availableCameras();
  runApp(new LibrarianApp());
} 

class LibrarianApp extends StatelessWidget {

  @override
  Widget build(BuildContext context) {
    return new MaterialApp(
      theme: new ThemeData(
        brightness: Brightness.dark,
        primaryColorBrightness: Brightness.dark,
        fontFamily: "Agne"
      ),
      title: 'Virtual Librarian',
      debugShowCheckedModeBanner: false,
      home: SplashScreen(),//LoginPage(),//(cameras),//SplashScreen(cameras),//LoginPage(cameras),
      routes: routes,
    ); 
  }
}