import 'dart:async';

import 'package:flutter/material.dart';
import 'package:virtual_librarian/login_screen.dart';
import 'package:animated_text_kit/animated_text_kit.dart';
import 'package:virtual_librarian/register_screen.dart';


class SplashScreen extends StatefulWidget {
  static String tag = 'splash-screen';
  @override
  _SplashScreenState createState() => _SplashScreenState();
}

class _SplashScreenState extends State<SplashScreen> {
  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    Timer(Duration(seconds: 3), () => Navigator.of(context).pushNamed(LoginPage.tag));
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: SizedBox(
          //width: 250.0,
          child: TypewriterAnimatedTextKit(
            duration: Duration(seconds: 3),
            onTap: () {
                print("Tap Event");
              },
            text: [
              "Virtual Librarian",
            ],
            textStyle: TextStyle(
                fontSize: 30.0,
                //fontFamily: "Agne"
            ),
          ),
        )
      ) 
    );
  }
}