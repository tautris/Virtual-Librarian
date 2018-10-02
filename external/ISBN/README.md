# Unclazz.Commons.Isbn

`Unclazz.Commons.Isbn` はISBNコードをシリアライズもしくはデシリアライズするためのライブラリです。
`ISBN-10` および `ISBN-13` の双方をサポートしています。
アセンブリは[NuGet Galleryで公開](https://www.nuget.org/packages/Unclazz.Commons.Isbn/)されているので、NuGetを通じて取得することができます。

## 名前空間

このライブラリのコンポーネントはアセンブリ名と同じ `Unclazz.Commons.Isbn` で公開されています。

## クラス

`IsbnCode` クラスはこのライブラリの主要コンポーネントであり、その名の通りISBNコードを表すクラスです。
そのインスタンスは `IsbnCode.Parse(string)` もしくは `IsbnCode.TryParse(string, out IsbnCode)`　を通じて得られます。
文字列の先頭に `"ISBN-13: "` や `"ISBN-10: "` 、はたまた  `"ISBN-13 "` や `"ISBN10: "` といったプレフィクスが付いていても（いなくても）問題ありません。コード本体を示す文字列に含まれる数字以外の文字はハイフン記号を含めすべて無視されます（ただし右端の検査数字には数字のほか `'X'` が使用可能）。

こうして得られた `IsbnCode` インスタンスのプロパティを通じて、ISBNコードを構成する各部分の数値にアクセスすることができます。
また `IsbnCode#ToString()` もしくは `IsbnCode#ToString(IsbnCodeStyles)` メソッドによりISBNコードの文字列表現を得られます。

`Digits` クラスはISBNコードを構成する各部分の数字のシーケンスを表現するためのクラスです。
`IsbnCode#Group` や `IsbnCode#Publisher` といった各種プロパティは `Digits` インスタンスを返します。
ISBNコードの各部分は、国・地域・言語圏とその出版者ごとに規定された長さ（桁数）を持っており、数値の左側にはその必要に応じてゼロ詰めが行われます。
このクラスを通じて、それらの予め規定された長さを持つ数字シーケンスの文字列表現もしくは整数表現にアクセスすることができます。

`IsbnCodeStyles` は `IsbnCode#ToString(IsbnCodeStyles)` メソッドによりISBNコードのデシリアライズを行う際に、そのスタイルを指定するための列挙型です。
この列挙型は名前から察せられる通り、 `[Flags]` 属性を付与されており、ビット演算によるスタイル指定を行えるようになっています。

## 使い方

```cs
// P・ブルデュー『社会学の社会学』のISBNコード
var isbnString = "ISBN-13: 978-4938661236";
var isbnCode = IsbnCode.Parse(isbnString);

// 以下のコードもこれと同義
// var isbnCode = IsbnCode.Parse("ISBN-13: 978-4-938661-23-6");
// var isbnCode = IsbnCode.Parse("ISBN-13 978-4938661236");
// var isbnCode = IsbnCode.Parse("ISBN 978-4-938661-23-6");
// var isbnCode = IsbnCode.Parse("ISBN978-4-938661-23-6");
// var isbnCode = IsbnCode.Parse("978-4-938661-23-6");
// var isbnCode = IsbnCode.Parse("9784938661236");
// var isbnCode = IsbnCode.Parse("ISBN-10: 4-938661-23-6");
// var isbnCode = IsbnCode.Parse("4-938661-23-6");
// var isbnCode = IsbnCode.Parse("4938661236");

// グループ記号は日本国内の出版であることを示す "4"
var group = isbnCode.Group; // => Digits("4")
var agencyName = isbnCode.Agency; // => "Japan"

// 出版者記号は藤原書店を表す "938661"
var publisher = isbnCode.Publisher; // => Digits("938661")

// 書名記号は 『社会学の社会学』 を示す "23"
var title = isbnCode.Title; // => Digits("23")

// 検査数字はモジュラス10で計算された '6'
var checkDigit = isbnCode.CheckDigit; // => '6'

// 引数なしの ToString() はプレフィクスやハイフンを含んだ形式の文字列を返す
var isbnString2 = isbnCode.ToString(); // => "ISBN-13: 978-4-938661-23-6"
var isbnString3 = isbnCode.ToString(IsbnCodeStyles.WithHyphens); // =>  "978-4-938661-23-6"
var isbnString4 = isbnCode.ToString(IsbnCodeStyles.AsIsbn10Code); // => "4-938661-23-6"
```

## グループとレンジ

ISBNのフラグ、グループ記号、そして出版者記号の値は、ISBNコードの管理団体により割り振られます。
そしてISBNコード全体の長さ（桁数）は固定されているので、グループ記号は出版者記号と書名記号の長さ（桁数）を、出版者記号は書名記号の長さ（桁数）を規定します。
このような次第でISBNコードを解析するには管理団体が管理するグループとそのグループ内で出版者記号が取りうる値の範囲の情報を入力とする必要があります。

以上から、 `Unclazz.Commons.Isbn` はそのプロジェクト・フォルダ直下に `RangeMessage.xml` というXMLファイルを格納し、
ビルドが行われるたびこのXMLファイルがアセンブリとともにビルド成果物のフォルダにコピーされるようにしています。
アセンブリ（*.dll）は自身の置かれているのと同じフォルダにXMLファイルが存在することを前提に動作します。
このXMLファイルの最新版は国際ISBNエージェンシーのWebサイトの [ISBN Ranges](https://www.isbn-international.org/range_file_generation) から入手可能です。
