import 'dart:ui';
import 'package:flutter/material.dart';
import 'package:camera/camera.dart';

import 'package:virtual_librarian/modules/login/login_presenter.dart';
import 'package:virtual_librarian/utils/bubble_painter.dart';
import 'package:virtual_librarian/views/home_screen.dart';

class LoginPage extends StatefulWidget {
  static String tag = 'login-page';
  @override
  _LoginPageState createState() => new _LoginPageState();
}

class _LoginPageState extends State<LoginPage> with SingleTickerProviderStateMixin implements LoginViewContract {
  LoginPresenter _presenter;

  bool _isCameraLoading = true;

  final GlobalKey<ScaffoldState> _scaffoldKey = new GlobalKey<ScaffoldState>();

  CameraController cameraController;
  PageController _pageController;

  Color leftTabColor = Colors.black;
  Color rightTabColor = Colors.white;

  _LoginPageState() {
    _presenter = new LoginPresenter(this);
  }

  @override
  void initState() {
    super.initState();
    _pageController = PageController();
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
    _pageController?.dispose();
    cameraController?.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {

    final face = Hero(
      tag: 'UserFace',
      child: _isCameraLoading ? 
      Container(
        width: 250.0,
        height: 250.0,
        padding: new EdgeInsets.only(left: 25.0, right: 25.0),
        child:  new CircularProgressIndicator(),
      )
      : Container (
        width: 250.0,
        height: 250.0,
        child: ClipOval(
          child: new AspectRatio(
            aspectRatio: 0.5625,
            child: new CameraPreview(cameraController)
          ),
        ),
      )
    );

    return new Scaffold(
      key: _scaffoldKey,
      body: NotificationListener<OverscrollIndicatorNotification>(
        onNotification: (overscroll) {
          overscroll.disallowGlow();
        },
        child: SingleChildScrollView(
          child: Container(
            width: MediaQuery.of(context).size.width,
            height: MediaQuery.of(context).size.height >= 775.0
                ? MediaQuery.of(context).size.height
                : 775.0,
            color: Color(0x22FFEEFF),
            child: Column(
              mainAxisSize: MainAxisSize.max,
              children: <Widget>[
                Padding(
                  padding: EdgeInsets.only(top: 110),
                  child: face,
                ),
                Padding(
                  padding: EdgeInsets.only(top: 50.0),
                  child: _buildMenuBar(context),
                ),
                Expanded(
                  flex: 2,
                  child: PageView(
                    controller: _pageController,
                    onPageChanged: (i) {
                      if (i == 0) {
                        setState(() {
                          leftTabColor = Colors.black;
                          rightTabColor = Colors.white;
                        }); 
                      } else if (i == 1) {
                        setState(() {
                          leftTabColor = Colors.white;
                          rightTabColor = Colors.black;      
                        });
                      }
                    },
                    children: <Widget>[
                      new ConstrainedBox(
                        constraints: const BoxConstraints.expand(),
                        child: _signInBuild(context),
                      ),
                      new ConstrainedBox(
                        constraints: const BoxConstraints.expand(),
                        child: _signUpBuild(context),
                      )
                    ],
                  ),
                ),
              ],
            ),
          ),
        )
      )
    );
  }
  
  Widget _buildMenuBar(BuildContext context) {
    return Container(
      width: 300.0,
      height: 50.0,
      decoration: BoxDecoration(
        color: Color(0x552B2B2B),
        borderRadius: BorderRadius.all(Radius.circular(25.0)),
      ),
      child: CustomPaint(
        painter: TabIndicationPainter(pageController: _pageController),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
          children: <Widget>[
            Expanded(
              child: FlatButton(
                splashColor: Colors.transparent,
                highlightColor: Colors.transparent,
                onPressed: _onSignInButtonPress,
                child: Text(
                  "Login",
                  style: TextStyle(
                      color: leftTabColor,
                      fontSize: 16.0,
                  )
                ),
              ),
            ),
            Expanded(
              child: FlatButton(
                splashColor: Colors.transparent,
                highlightColor: Colors.transparent,
                onPressed: _onSignUpButtonPress,
                child: Text(
                  "Register",
                  style: TextStyle(
                      color: rightTabColor,
                      fontSize: 16.0,
                  )
                ),
              ),
            ),
          ],
        )
      ),
    );
  }

  Widget _signInBuild (BuildContext context) {
    return Container(
      padding: EdgeInsets.only(top: 70),
      child: Column(
        children: <Widget>[
          Stack(
            alignment: Alignment.topCenter,
            overflow: Overflow.visible,
            children: <Widget>[
              Card(
                elevation: 2.0,
                color: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(5.0)
                ),
                child: Container(
                  width: 300,
                  height: 100,
                  child: Column(
                    children: <Widget>[
                      Padding(
                        padding: EdgeInsets.symmetric(vertical: 20.0, horizontal: 25.0),
                        child: TextField(
                          style: TextStyle(
                            fontSize: 16.0,
                            color: Colors.black
                          ),
                          decoration: InputDecoration(
                            border: InputBorder.none,
                            icon: Icon(
                              Icons.label_important,
                              color: Color(0xFF4BDDB9),
                              size: 22.0,
                            ),
                            hintText: "NickName",
                            hintStyle: TextStyle(color: Colors.black45),
                          ),
                        ),
                      ),
                    ],
                  ),
                ),
              ),
              Container(
                margin: EdgeInsets.only(top: 80.0),
                decoration: new BoxDecoration(
                  borderRadius: BorderRadius.all(Radius.circular(5.0)),
                  boxShadow: <BoxShadow>[
                    BoxShadow(
                      color: Color(0xFF6BFDD9),
                      offset: Offset(1.0, 2.0),
                      blurRadius: .5,
                    ),
                  ],
                  color: Color(0xFF666666)
                ),
                child: MaterialButton(
                    highlightColor: Colors.transparent,
                    splashColor: Colors.transparent,
                    child: Padding(
                      padding: const EdgeInsets.symmetric(
                          vertical: 10.0, horizontal: 42.0),
                      child: Text(
                        "LOGIN",
                        style: TextStyle(
                            color: Colors.white,
                            fontSize: 25.0,
                        ),
                      ),
                    ),
                    onPressed:(){
                      Navigator.of(context).pushNamed(HomeScreenState.tag);
                    }
                )
              ),
            ],
          ),
        ],
      )
    );
  }

  Widget _signUpBuild (BuildContext context) {
    return Container(
      padding: EdgeInsets.only(top: 20),
      child: Column(
        children: <Widget>[
          Stack(
            alignment: Alignment.topCenter,
            overflow: Overflow.visible,
            children: <Widget>[
              Card(
                elevation: 2.0,
                color: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(5.0)
                ),
                child: Container(
                  width: 300,
                  height: 200,
                  child: Column(
                    children: <Widget>[
                      Padding(
                        padding: EdgeInsets.symmetric(vertical: 20.0, horizontal: 25.0),
                        child: TextField(
                          style: TextStyle(
                            fontSize: 16.0,
                            color: Colors.black
                          ),
                          decoration: InputDecoration(
                            border: InputBorder.none,
                            icon: Icon(
                              Icons.label_important,
                              color: Color(0xFF4BDDB9),
                              size: 22.0,
                            ),
                            hintText: "NickName",
                            hintStyle: TextStyle(color: Colors.black45),
                          ),
                        ),
                      ),
                      Container(
                        width: 250.0,
                        height: 1.0,
                        color: Colors.grey[400],
                      ),
                      Padding(
                        padding: EdgeInsets.symmetric(vertical: 20.0, horizontal: 25.0),
                        child: TextField(
                          style: TextStyle(
                            fontSize: 16.0,
                            color: Colors.black),
                          decoration: InputDecoration(
                            border: InputBorder.none,
                            icon: Icon(
                              Icons.lock,
                              color: Color(0xFF4BDDB9),
                              size: 22.0,
                            ),
                            hintText: "Password",
                            hintStyle: TextStyle(color: Colors.black45),
                          ),
                        ),
                      )
                    ],
                  ),
                ),
              ),
              Container(
                margin: EdgeInsets.only(top: 170.0),
                decoration: new BoxDecoration(
                  borderRadius: BorderRadius.all(Radius.circular(5.0)),
                  boxShadow: <BoxShadow>[
                    BoxShadow(
                      color: Color(0xFF6BFDD9),
                      offset: Offset(1.0, 2.0),
                      blurRadius: .5,
                    ),
                  ],
                  color: Color(0xFF666666)
                ),
                child: MaterialButton(
                    highlightColor: Colors.transparent,
                    splashColor: Colors.transparent,
                    child: Padding(
                      padding: const EdgeInsets.symmetric(
                          vertical: 10.0, horizontal: 42.0),
                      child: Text(
                        "REGISTER",
                        style: TextStyle(
                            color: Colors.white,
                            fontSize: 25.0,
                        ),
                      ),
                    ),
                    onPressed:(){
                          _pageController.animateToPage(0,
                            duration: Duration(milliseconds: 500), curve: Curves.decelerate);
                    }
                )
              ),
            ],
          ),
        ],
      )
    );
  }

  void _onSignInButtonPress() {
    _pageController.animateToPage(0,
        duration: Duration(milliseconds: 500), curve: Curves.decelerate);
  }

  void _onSignUpButtonPress() {
    _pageController?.animateToPage(1,
        duration: Duration(milliseconds: 500), curve: Curves.decelerate);
  }
}