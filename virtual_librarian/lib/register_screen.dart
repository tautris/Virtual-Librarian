import 'package:flutter/material.dart';
import 'package:virtual_librarian/home_screen.dart';
import 'package:percent_indicator/percent_indicator.dart';

class RegisterPage extends StatefulWidget {
  static String tag = 'register-page';
  @override
  State<StatefulWidget> createState() {
    return _RegisterPageState();
  }
}

class _RegisterPageState extends State<RegisterPage> {
  var _progress = 0.0;
  var _forms = true;

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

    final progressIndicator = LinearPercentIndicator(
      width: MediaQuery.of(context).size.width - 60,
      //animation: true,
      lineHeight: 14.0,
      //animationDuration: 2500,
      percent: _progress,
      linearStrokeCap: LinearStrokeCap.roundAll,
      progressColor: Color.fromARGB(125, 120, 72, 72),
    );

    final email = TextFormField(
      keyboardType: TextInputType.emailAddress,
      autofocus: false,
      decoration: InputDecoration(
        hintText: 'Email',
        contentPadding: EdgeInsets.fromLTRB(20.0, 10.0, 20.0, 10.0),
        border: OutlineInputBorder(borderRadius: BorderRadius.circular(32.0)),
        fillColor: Colors.white,
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

    final pin = TextFormField(
     keyboardType: TextInputType.number,
      autofocus: false,
      decoration: InputDecoration(
        hintText: 'PIN',
        contentPadding: EdgeInsets.fromLTRB(20.0, 10.0, 20.0, 10.0),
        border: OutlineInputBorder(borderRadius: BorderRadius.circular(32.0)),
        fillColor: Colors.white,
      ),
    );

    final nextButton = ButtonTheme (
      height: 50.0,
      child: RaisedButton(
        elevation: 20,
        color: Color.fromARGB(255, 60, 112, 112),
        splashColor: Colors.white54,
        textColor: Colors.white,
        shape: new RoundedRectangleBorder(borderRadius: new BorderRadius.circular(30.0)),
        child: new Icon(Icons.navigate_next),
        onPressed: () {
          setState(() {
            _forms = false;
          });
        },
      ),
    );

    final enabledFinishButton = ButtonTheme (
      height: 50.0,
      child: RaisedButton(
        elevation: 20,
        color: Color.fromARGB(255, 60, 112, 112),
        splashColor: Colors.white54,
        textColor: Colors.white,
        shape: new RoundedRectangleBorder(borderRadius: new BorderRadius.circular(30.0)),
        child: new Icon(Icons.check),
        onPressed: () {
          if (_progress >= 1.0) {
            Navigator.of(context).pushNamed(HomeScreenState.tag);
          } else {
            setState(() {
              _progress=_progress + 0.1;
            });
          }
        },
      ),
    );

    Widget childView;
    if (_forms) {
      childView = (
        new Scaffold (
          body: Container (
            decoration: new BoxDecoration(
              gradient: new LinearGradient(
                begin: FractionalOffset.topCenter,
                end: FractionalOffset.bottomCenter,
                colors: [Color (0xFFFFFF), Color(0xCFCFCF)],//[const Color(0xFF915FB5), const Color(0xFFCA436B)],
                stops: [0.0, 1.0],
              )
            ),
            child: Center(
              child: ListView(
                shrinkWrap: true,
                padding: EdgeInsets.only(left: 30.0, right: 30.0),
                children: <Widget>[
                  email,
                  SizedBox(height: 20.0),
                  nickname,
                  SizedBox(height: 20.0),
                  pin,
                  SizedBox(height: 20.0),
                  pin,
                  SizedBox(height: 50.0),
                  nextButton,
                ],
              ),
            ),
          ),
        )
      );
    } else {
      childView = (
        new Scaffold (
          body: Container (
            decoration: new BoxDecoration(
              gradient: new LinearGradient(
                begin: FractionalOffset.topCenter,
                end: FractionalOffset.bottomCenter,
                colors: [Color (0xFFFFFF), Color(0xCFCFCF)],//[const Color(0xFF915FB5), const Color(0xFFCA436B)],
                stops: [0.0, 1.0],
              )
            ),
            child: Center(
              child: ListView(
                shrinkWrap: true,
                padding: EdgeInsets.only(left: 30.0, right: 30.0),
                children: <Widget>[
                  face,
                  SizedBox(height: 5),
                  progressIndicator,
                  SizedBox(height: 50),
                  enabledFinishButton
                ],
              ),
            ),
          ),
        )
      );
    }
    return childView;
  }
}