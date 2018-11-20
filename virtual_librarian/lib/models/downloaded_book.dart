class DownloadedBook {
  final int id;
  final String author;
  final String title;
  final String imageURL;

  DownloadedBook(this.id, this.author, this.title, this.imageURL);

  DownloadedBook.fromJson(Map<String, dynamic> json)
    : id = json['id'],
      author = json['author'],
      title = json['title'],
      imageURL = json['image'];
}