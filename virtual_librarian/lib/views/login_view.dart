import 'dart:math';
import 'dart:ui';
import 'package:flutter/material.dart';
import 'package:camera/camera.dart';

import 'package:virtual_librarian/modules/login/login_presenter.dart';
import 'package:virtual_librarian/views/home_screen.dart';
import 'package:virtual_librarian/views/register_screen.dart';

class LoginPage extends StatefulWidget {
  static String tag = 'login-page';
  @override
  _LoginPageState createState() => new _LoginPageState();
}

class _LoginPageState extends State<LoginPage> implements LoginViewContract {
  LoginPresenter _presenter;

  bool _isCameraLoading = true;

  CameraController cameraController;

  _LoginPageState() {
    _presenter = new LoginPresenter(this);
  }

  @override
  void initState() {
    super.initState();
    _presenter.loadCameras();
  }

  @override
  void onLoadCameraComplete(List<CameraDescription> cameras) {
    cameraController = new CameraController(cameras[1], ResolutionPreset.high);
    cameraController.initialize().then((_) {
      if (!mounted) {
        return;
      }
      setState(() {
        _isCameraLoading = false;
      });
    });
  }

  @override
  void onLoadCameraError() {
    print("VIEW. Load Camera error happened");
  }

  @override
  void dispose() {
    cameraController?.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final face = Hero(
      tag: 'UserFace',
      child: _isCameraLoading ? 
      Container(
        height: 250.0,
        padding: new EdgeInsets.only(left: 25.0, right: 25.0),
        child:  new CircularProgressIndicator(),
      )
      : Container (
        child: ClipOval(
          child: new CustomPaint (
            foregroundPainter:  new GuidelinePainter(),
            child: new AspectRatio(
              aspectRatio: 0.5625, //TODO: Fix aspect Ratio, maybe controller.value.aspectRatio,
              child: new CameraPreview(cameraController)
            ),
          )
        ),
      )
    );

    final loginButton = ButtonTheme (
        height: 50.0,
        child: RaisedButton(
          elevation: 20,
          color: Color(0xBB6BFDD9),
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
            colors:  [Color (0x22FFEEFF), Color(0x88FFEEFF)],
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