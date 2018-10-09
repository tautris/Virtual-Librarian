using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace Virtual_Librarian
{
    [Table(Name = "Authors")]
    class Author
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "ID")] private int id;
        [Column] public string Name { get; private set; }
        [Column] public string Surname { get; private set; }

        private EntitySet<AuthorBook> _authorBooks = new EntitySet<AuthorBook>();

        [Association(Name = "IDToAuthor", Storage = "_authorBooks", OtherKey = "authorID", ThisKey = "id")]
        private ICollection<AuthorBook> authorBooks
        {
            get => _authorBooks;
            set => _authorBooks.Assign(value);
        }
    }


    [Table(Name = "BOOKS")]
    class Book
    {
        [Column] public string title { get; private set; }
        [Column(IsPrimaryKey = true)] public string ISBN { get; private set; }
        [Column] public short publishYear { get; private set; }
        [Column] public string publisher { get; private set; }

        private EntitySet<AuthorBook> _authorBooks = new EntitySet<AuthorBook>();

        [Association(Name = "ISBNToBook", Storage = "_authorBooks", OtherKey = "ISBN", ThisKey = "ISBN")]
        private ICollection<AuthorBook> authorBooks
        {
            get => _authorBooks;
            set => _authorBooks.Assign(value);
        }


        public List<Author> Authors => (from auths in authorBooks select auths.author).ToList();


        public Book() { } //needed for LINQ
        public Book(string ISBN, string title, short publishYear, Author author)
        {
            this.title = title;
            //this.Authors;
            this.publishYear = publishYear;
            if (!Validators.IsValidISBN(ISBN))
            {
                throw new ArgumentException("ISBN is not valid");
            }
            else
            {
                this.ISBN = ISBN.Replace("-", "");
            }
        }

        public override string ToString()
        {
            //string retISBN = ISBN.Length == 13 ? IsbnCode.Parse(ISBN).ToString(IsbnCodeStyles.WithHyphens) : IsbnCode.Parse(ISBN).ToString(IsbnCodeStyles.AsIsbn10Code);
            //string authorlist = string.Join("\n", Authors.Select(a => a.Name[0].ToString() + ". " + a.Surname));



            return this.ISBN;

        }

    }


    [Table]
    internal class AuthorBook //used for mapping books to authors and back for DB
    {
        [Column(IsPrimaryKey = true, Name = "AUTHORID")]
        private int authorID;
        [Column(IsPrimaryKey = true)]
        private string ISBN;

        private EntityRef<Author> _author = new EntityRef<Author>();
        [Association(Name = "IDToAuthor", IsForeignKey = true, Storage = "_author", ThisKey = "authorID")]
        public Author author
            {
                get => _author.Entity;
                set => _author.Entity = value;
            }




        private EntityRef<Book> _book = new EntityRef<Book>();

        [Association(Name = "ISBNToBook", IsForeignKey = true, Storage = "_book", ThisKey = "ISBN")]
        public Book book
            {
                get => _book.Entity;
                set => _book.Entity = value;
            }


    }
}

