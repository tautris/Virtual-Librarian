import 'package:flutter/material.dart';

import 'package:virtual_librarian/views/tabs/books_feed_view.dart';
import 'package:virtual_librarian/views/tabs/books_downloaded_view.dart';

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
      'Book Feed': new BookFeed(),
      'My Books': new MyBooks(),
      'My Stats': new Center(
        child: new Text('Graphs. Future Release 1.1'),
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
                colors:  [Color (0x22FFEEFF), Color(0x88FFEEFF)],
                stops: [0.0, 1.0],
              )
          ),
        ),
        new Scaffold(
          backgroundColor: const Color(0x00000000),
          appBar: new AppBar(
            backgroundColor: const Color(0x00000000),
            elevation: 0.0,
            leading: new Center(
              child: new ClipOval(
                child: Image.asset('assets/profile.gif'),
              ),
            ),
            actions: [
              new IconButton(
                icon: new Icon(Icons.search),
                onPressed: () {
                  showSearch(context: context, delegate: DataSearch());
                },
              ),
            ],
            title: const Text('Virtual Librarian'),
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

class DataSearch extends SearchDelegate<String> {
  final books = [
    "notes from underground",
    "1984",
    "aaaaa",
    "Animal Farm",
    "Etiquete",
  ];

  final recentBooks = [
    "Animal Farm",
    "Etiquete",
  ];

  @override 
  ThemeData appBarTheme(BuildContext context) {
    assert(context != null);
    final ThemeData theme = Theme.of(context);
    assert(theme != null);
    return theme.copyWith(
      primaryColor: Color(0x44FFEEFF),
      primaryIconTheme: theme.primaryIconTheme.copyWith(color: Colors.white),
      primaryColorBrightness: Brightness.dark,
      primaryTextTheme: theme.textTheme,
    );
  }

  @override
  List<Widget> buildActions(BuildContext context) {
    return [IconButton(icon: Icon(Icons.clear), onPressed: (){
      if (query.isEmpty) {
        close(context, null);
      } else {
        query = "";
      }
    })];
  }

  @override
  Widget buildLeading(BuildContext context) {
    return IconButton(
      icon: AnimatedIcon(
        icon: AnimatedIcons.menu_arrow,
        progress: transitionAnimation,
      ),
      onPressed: (){
        close(context, null);
      },
    );
  }

  @override
  Widget buildResults(BuildContext context) {
    return Center(
      child: Container(
        height: 100.0,
        width: 100.0,
        child: Card(
          color: Colors.red,
          shape: StadiumBorder(),
          child: Center(
            child: Text(query),
          ),
        )
      )
    );
  }

  @override
  Widget buildSuggestions(BuildContext context) {
    final suggestionList = query.isEmpty 
    ? recentBooks 
    : books.where((b) => b.toString().toLowerCase().contains(query.toLowerCase())).toList();

    return ListView.builder(
      itemBuilder: (context, index) => ListTile(
        onTap: () {
          showResults(context);
        },
        leading: Icon(Icons.book),
        title: RichText(
          text: TextSpan(
            text: suggestionList[index].substring(0, query.length),
            style: TextStyle(
              color: Color(0xBB6BFDD9), fontWeight: FontWeight.bold),
              children: [
                TextSpan(
                  text: suggestionList[index].substring(query.length),
                  style: TextStyle(color: Colors.grey)
                ),
              ]
            )
          ),
      ),
      itemCount: suggestionList.length,
    );
  }}