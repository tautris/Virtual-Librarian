import 'dart:ui';

import 'package:flutter/material.dart';
import 'package:virtual_librarian/home_screen.dart';
import 'package:virtual_librarian/register_screen.dart';

class LoginPage extends StatefulWidget {
  static String tag = 'login-page';
  @override
  _LoginPageState createState() => new _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  @override
  Widget build(BuildContext context) {
    final face = Hero(
      tag: 'UserFace',
      child: CircleAvatar(
        backgroundColor: Colors.black,
        radius: 150.0,
        child: new CircularProgressIndicator(),//Image.asset('assets/login_icon.png'),
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
            begin: FractionalOffset.topCenter,
            end: FractionalOffset.bottomCenter,
            colors: [
              const Color.fromARGB(55, 120, 72, 72),
              const Color.fromARGB(155, 187, 85, 99),
            ],
            stops: [0.0, 1.0],
          )
        ),
        child: Center(
          child: ListView(
          shrinkWrap: true,
          padding: EdgeInsets.only(left: 30.0, right: 30.0),
          children: <Widget>[
            face,
            SizedBox(height: 20.0),
            nickname,
            SizedBox(height: 24.0),
            loginButton,
            registerLabel,
          ],
        ),
        ),
      ),
    );

    /*return new Container(
          decoration: new BoxDecoration(
            gradient: new LinearGradient(
              begin: FractionalOffset.topCenter,
              end: FractionalOffset.bottomCenter,
              colors: [
                const Color.fromARGB(55, 255, 72, 72),
                const Color.fromARGB(155, 87, 155, 149),
              ],
              stops: [0.0, 1.0],
            )
          ),
          child: new Align(
            child: new Container(
              child: Center(
                child: ListView(
                  shrinkWrap: true,
                  padding: EdgeInsets.only(left: 30.0, right: 30.0),
                  children: <Widget>[
                    logo,
                    //email,
                    loginButton,
                    // SizedBox(height: 60.0),
                    // email,
                  ],
                ),
              ),
            ),
            // child: ListView(
            //   shrinkWrap: true,
            //   children: <Widget>[
            //     logo,
            //     SizedBox(height: 60.0),
            //     email,
            //     SizedBox(height: 8.0),
            //     loginButton,
            //     forgotLabel,
            //   ],
            // )
          ),
    );
          // child: new Scaffold(
          //   body: Center(
          //     child: ListView(
          //       shrinkWrap: true,
          //       padding: EdgeInsets.only(left: 30.0, right: 30.0),
          //       children: <Widget>[
          //         logo,
          //         SizedBox(height: 60.0),
          //         email,
          //         SizedBox(height: 8.0),
          //         password,
          //         SizedBox(height: 24.0),
          //         loginButton,
          //         forgotLabel
          //       ],
          //     ),
          //   ),
          // )

        //   child: new Align(
        //     alignment: FractionalOffset.bottomCenter,
        //     child: new Container(
        //       padding: const EdgeInsets.all(10.0),
        //       child: new Text(
        //         'B  O  O  K  S',
        //         style: textTheme.headline.copyWith(
        //           color: Colors.grey.shade800.withOpacity(0.8),
        //           fontWeight: FontWeight.bold,
        //         ),
        //       ),
        //     )
        //   )
        // );

    // return Scaffold(
    //   backgroundColor: Colors.white,
    //   body: Center(
    //     child: ListView(
    //       shrinkWrap: true,
    //       padding: EdgeInsets.only(left: 30.0, right: 30.0),
    //       children: <Widget>[
    //         logo,
    //         SizedBox(height: 60.0),
    //         email,
    //         SizedBox(height: 8.0),
    //         password,
    //         SizedBox(height: 24.0),
    //         loginButton,
    //         forgotLabel
    //       ],
    //     ),
    //   ),
    // );
    */
  }
}