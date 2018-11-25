import 'dart:io';
import 'package:flutter/material.dart';
import 'package:http/http.dart';
import 'dart:convert';

import 'package:open_file/open_file.dart';
import 'package:path_provider/path_provider.dart';
import 'package:path/path.dart';

import 'package:virtual_librarian/models/downloaded_book.dart';

class MyLikesState extends StatefulWidget {
  @override
  State<StatefulWidget> createState() {
    // TODO: implement createState
    return MyLikesView(); 
  }
}

class MyLikesView extends State<MyLikesState> {

  Future<List<DownloadedBook>> _fetchDownloadedBooks () async {
    var dir = await getApplicationDocumentsDirectory();
    var pdfFolderDir = new Directory("${dir.path}/pdf");
    var _filesPDF = pdfFolderDir.listSync(recursive: false, followLinks: false);
    var fileslist = _filesPDF.toList();
    List<String> pdfList = new List();
    fileslist.forEach((file){
      String filename = basename(file.path);
      pdfList.add(filename.replaceAll(".pdf", ""));
    });

    final url = "http://192.168.0.19:8081/GetBook";
    var client = new Client();
    List<Response> bookList = await Future.wait(pdfList.map((bookId) => client.get("$url/$bookId")));
    return bookList.map((response){
      return new DownloadedBook.fromJson(json.decode(response.body));
    }).toList();
  }

  @override
  Widget build(BuildContext context) {
    Widget childView;
    childView = FutureBuilder(
      future: _fetchDownloadedBooks(),
      builder: (BuildContext context, AsyncSnapshot snapshot) {
        if (snapshot.hasData) {
          if (snapshot.data!=null) {
            return ListView.builder(
              itemCount: snapshot.data != null ? snapshot.data.length : 0,
              itemBuilder: (context, i) {
                final book = snapshot.data[i];
                return new LikedBooks(id: book.id, title: book.title, author: book.author, imageURL: book.imageURL);
              }
            );
          }
        } else {
          return CircularProgressIndicator();
        }
      }
    );
    return childView;
  }
}

class LikedBooks extends StatelessWidget {
  const LikedBooks({ this.id, this.title, this.author, this.imageURL });

  final int id;
  final String title;
  final String author;
  final String imageURL;

  @override
  Widget build(BuildContext context) {
    TextTheme textTheme = Theme
      .of(context)
      .textTheme;
    return new GestureDetector (
      onTap: () async {
        var dir = await getApplicationDocumentsDirectory();
        var fileName = id;
        var path = "${dir.path}/$fileName.pdf";
        //FIXME: check if file exists
        if (FileSystemEntity.typeSync(path) != FileSystemEntityType.notFound) {
          try {
            Scaffold.of(context).showSnackBar(new SnackBar(
              duration: Duration(seconds: 1),
              content: new Text("Opening PDF..."),
            ));
            OpenFile.open("${dir.path}/pdf/$fileName.pdf");
          } catch (e) {
            print (e);
          }
        } else {
          Scaffold.of(context).showSnackBar(new SnackBar(
            duration: Duration(seconds: 1),
            content: new Text("$title PDF File does not exist"),
          ));
        }
      },
      child: Container(
        margin: const EdgeInsets.symmetric(horizontal: 10.0, vertical: 5.0),
        padding: const EdgeInsets.symmetric(horizontal: 15.0, vertical: 10.0),
        decoration: new BoxDecoration(
          color: Colors.grey.shade200.withOpacity(0.3),
          borderRadius: new BorderRadius.circular(5.0),
        ),
        child: new IntrinsicHeight(
          child: new Row(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: <Widget>[
              new Container(
                margin: const EdgeInsets.only(top: 4.0, bottom: 4.0, right: 10.0),
                child: new CircleAvatar(
                  // backgroundImage: new NetworkImage(
                  //   'http://thecatapi.com/api/images/get?format=src'
                  //     '&size=small&type=jpg#${title.hashCode}'
                  // ),
                  radius: 20.0,
                ),
              ),
              new Expanded(
                child: new Container(
                  child: new Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: <Widget>[
                      new Text(title, style: textTheme.subhead),
                      new Text(author, style: textTheme.caption),
                    ],
                  ),
                ),
              ),
              new Container(
                margin: new EdgeInsets.symmetric(horizontal: 5.0),
                child: new InkWell(
                  child: new Icon(Icons.delete_sweep, size: 40.0),
                  onTap: () async {
                    //FIXME: check if file exists
                    var dir = await getApplicationDocumentsDirectory();
                    var fileName = this.id;
                    var path = "${dir.path}/pdf/$fileName.pdf";
                    //FIXME: check if file exists
                    if (FileSystemEntity.typeSync(path) != FileSystemEntityType.notFound) {
                      Scaffold.of(context).showSnackBar(new SnackBar(
                        duration: Duration(seconds: 1),
                        content: new Text("Deleting $title.pdf from local memory (lol not)"),
                      ));
                      File(path).delete();
                    } else {
                      Scaffold.of(context).showSnackBar(new SnackBar(
                        duration: Duration(seconds: 1), 
                        content: new Text("Error. File was not found."),
                      ));
                    }
                  },
                ),
              ),
            ],
          ),
        ),
      )
    );
  }
}