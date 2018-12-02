import 'dart:math';
import 'dart:ui';
import 'package:flutter/material.dart';
import 'package:camera/camera.dart';

import 'package:virtual_librarian/views/home_screen.dart';
import 'package:virtual_librarian/views/register_screen.dart';

class LoginPage extends StatefulWidget {
  static String tag = 'login-page';

  List<CameraDescription> cameras;

  LoginPage(this.cameras);

  @override
  _LoginPageState createState() => new _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  CameraController controller;

  @override
  void initState() {
    super.initState();
    controller = new CameraController(widget.cameras[1], ResolutionPreset.high);
    controller.initialize().then((_) {
      if (!mounted) {
        return;
      }
      setState((){});
    });
  }

  @override
  void dispose() {
    controller?.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final face = Hero(
      tag: 'UserFace',
      child: Container(
        height: 250.0,
        padding: new EdgeInsets.only(left: 25.0, right: 25.0),
        child: ClipOval (
          child: new CustomPaint (
            foregroundPainter: new GuidelinePainter(),
            child: new AspectRatio(
              aspectRatio: 0.5625,//controller.value.aspectRatio,
              child: new CameraPreview(controller)
            )
          )
        )
      //new CircularProgressIndicator(),//Image.asset('assets/login_icon.png'),
      )
    );

    final loginButton = ButtonTheme (
        height: 50.0,
        child: RaisedButton(
          elevation: 20,
          color: Color.fromARGB(255, 60, 112, 112),
          splashColor: Colors.white54,
          textColor: Colors.white,
          shape: new RoundedRectangleBorder(borderRadius: new BorderRadius.circular(30.0)),
          child: new Icon(Icons.check),
          onPressed: () {
            Navigator.of(context).pushNamed(HomeScreenState.tag);
          },
        ),
    );

    final nickname = TextFormField(
      keyboardType: TextInputType.emailAddress,
      autofocus: false,
      decoration: InputDecoration(
        hintText: 'Nickname',
        contentPadding: EdgeInsets.fromLTRB(20.0, 10.0, 20.0, 10.0),
        border: OutlineInputBorder(borderRadius: BorderRadius.circular(32.0)),
        fillColor: Colors.white,
      ),
    );

    final registerLabel = FlatButton(
      child: Text(
        'Want to join?',
        style: TextStyle(color: Colors.white),
      ),
      onPressed: () {
        print("register");
        Navigator.of(context).pushNamed(RegisterPage.tag);
      },
    );

    return new Scaffold (
      body: Container(
        decoration: new BoxDecoration(
          gradient: new LinearGradient(
            begin: FractionalOffset.topLeft,
            end: FractionalOffset.bottomRight,
            colors:  [Color (0xFF3F8F8F), Color(0xCF3F3F3F)],
            stops: [0.0,1.0],
            tileMode: TileMode.clamp
          )
        ),
        child: Center(
          child: ListView(
          shrinkWrap: true,
          padding: EdgeInsets.only(left: 30.0, right: 30.0),
          children: <Widget>[
            face,
            SizedBox(height: 30.0),
            nickname,
            SizedBox(height: 20.0),
            loginButton,
            registerLabel,
          ],
        ),
        ),
      ),
    );
  }
}

class GuidelinePainter extends CustomPainter {
  @override
  void paint(Canvas canvas, Size size) {
    Paint line = new Paint()
      ..strokeWidth = 3.0
      ..color = Colors.white
      ..style = PaintingStyle.stroke
      ..strokeCap = StrokeCap.round
      ..blendMode = BlendMode.clear;

    Offset center = new Offset(size.width/2, size.height/2);
    double radius = min (size.width/2, size.height/2);
    
    //TODO: draw face lines

    //canvas.drawCircle(center, radius, line);
  }

  @override
  bool shouldRepaint(CustomPainter oldDelegate) => true;
}