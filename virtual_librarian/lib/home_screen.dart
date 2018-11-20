import 'package:flutter/material.dart';
import 'package:virtual_librarian/tabs/book_feed.dart';
import 'package:virtual_librarian/tabs/my_likes.dart';

class HomeScreenState extends StatelessWidget {
  static String tag = 'home-page';
  @override
  Widget build(BuildContext context) {
    return new HomeScreen();
  }
}

class CustomTabBar extends AnimatedWidget implements PreferredSizeWidget {
  CustomTabBar({ this.pageController, this.pageNames })
    : super(listenable: pageController);

  final PageController pageController;
  final List<String> pageNames;

  @override
  final Size preferredSize = new Size(0.0, 40.0);

  @override
  Widget build(BuildContext context) {
    TextTheme textTheme = Theme
      .of(context)
      .textTheme;
    return new Container(
      height: 40.0,
      margin: const EdgeInsets.all(10.0),
      padding: const EdgeInsets.symmetric(horizontal: 20.0),
      decoration: new BoxDecoration(
        color: Colors.grey.shade800.withOpacity(0.5),
        borderRadius: new BorderRadius.circular(20.0),
      ),
      child: new Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: new List.generate(pageNames.length, (int index) {
          return new InkWell(
            child: new Text(
              pageNames[index],
              style: textTheme.subhead.copyWith(
                color: Colors.white.withOpacity(
                  index == pageController.page || (pageController.page == null && index == 0) ? 1.0 : 0.2,
                ),
              )
            ),
            onTap: () {
              pageController.animateToPage(
                index,
                curve: Curves.easeOut,
                duration: const Duration(milliseconds: 300),
              );
            }
          );
        })
          .toList(),
      ),
    );
  }
}

class HomeScreen extends StatefulWidget {
  @override
  _HomeScreenState createState() => new _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {

  PageController _pageController = new PageController(initialPage: 0);

  @override
  build(BuildContext context) {
    final Map<String, Widget> pages = <String, Widget>{
      'Book Feed': new BookFeedState(),
      'My Books': new MyLikesState(),
      'My Stats': new Center(
        child: new Text('Graphs and something'),
      ),
    };
    TextTheme textTheme = Theme
      .of(context)
      .textTheme;
    return new Stack(
      children: [
        new Container(
            decoration: new BoxDecoration(
              gradient: new LinearGradient(
                begin: FractionalOffset.topCenter,
                end: FractionalOffset.bottomCenter,
                colors: [Color (0xFFFFFFFF), Color(0xFFAFAFAF)],//[const Color(0xFF915FB5), const Color(0xFFCA436B)],
                stops: [0.0, 1.0],
              )
          ),
          child: new Align(
            alignment: FractionalOffset.bottomCenter,
            child: new Container(
              padding: const EdgeInsets.all(10.0),
              child: new Text(
                'P  I  R  M  A  G  R  U  P  E',
                style: textTheme.headline.copyWith(
                  color:  Color.fromARGB(55, 20, 72, 72).withOpacity(0.8),//Colors.grey.shade800.withOpacity(0.8),
                  fontWeight: FontWeight.bold,
                ),
              ),
            )
          )
        ),
        new Scaffold(
          backgroundColor: const Color(0x00000000),
          appBar: new AppBar(
            backgroundColor: const Color(0x00000000),
            elevation: 0.0,
            leading: new Center(
              child: new ClipOval(
                child: Image.asset('assets/profile.jpg'),
              ),
            ),
            actions: [
              new IconButton(
                icon: new Icon(Icons.add),
                onPressed: () {
                  // TODO: implement
                },
              ),
            ],
            title: const Text('Users Books'),
            bottom: new CustomTabBar(
              pageController: _pageController,
              pageNames: pages.keys.toList(),
            ),
          ),
          body: new PageView(
            controller: _pageController,
            children: pages.values.toList(),
          ),
        ),
      ],
    );
  }
}