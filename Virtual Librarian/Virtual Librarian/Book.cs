using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Unclazz.Commons.Isbn;

namespace Virtual_Librarian
{
    [Table(Name ="Authors")]
    class Author
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "ID")] private int id { get; }
        [Column] public string Name { get; private set; }
        [Column] public string Surname { get;private  set; }

        private EntitySet<AuthorBook> _authorBooks = new EntitySet<AuthorBook>();

        [Association(Name = "IDToAuthor", Storage = "_authorBooks", OtherKey = "authorID", ThisKey = "id")]
        private ICollection<AuthorBook> authorBooks
        {
            get { return _authorBooks; }
            set { _authorBooks.Assign(value); }
        }

    }


    [Table(Name = "BOOKS")]
    class Book
    {
        [Column]                      public string  title { get; private set; }
        [Column(IsPrimaryKey =true)]  public string  ISBN { get; private set; }
        [Column] public short publishYear { get; private set; }
        [Column] public string publisher { get; private set; }

        private EntitySet<AuthorBook> _authorBooks = new EntitySet<AuthorBook>();

        [Association(Name="ISBNToBook",Storage="_authorBooks",OtherKey="ISBN",ThisKey="ISBN")]
        private ICollection<AuthorBook> authorBooks
        {
            get { return _authorBooks; }
            set { _authorBooks.Assign(value); }
        }

        
        public List<Author> Authors
        {
            get { return (from auths in authorBooks select auths.author).ToList();  }
        }

        







        public Book() { } //needed for LINQ
        public Book (string  ISBN, string title, short publishYear, Author author)
        {
            this.title= title;
            //this.Authors;
            this.publishYear = publishYear;
            if (!IsValidISBN(ISBN))
                throw new ArgumentException("ISBN is not valid");
            else
                this.ISBN = ISBN.Replace("-", "");

        }

        public override string ToString()
        {
            string retISBN = ISBN.Length == 13 ? IsbnCode.Parse(ISBN).ToString(IsbnCodeStyles.WithHyphens) : IsbnCode.Parse(ISBN).ToString(IsbnCodeStyles.AsIsbn10Code);
            string authorlist = string.Join("\n", Authors.Select(a => a.Name[0].ToString() + ". " + a.Surname));



            return String.Format("Title: {0}\nAuthors: {1}Publish year: {2}\nISBN: {3}\n", title, authorlist, publishYear, ISBN);

        }


        public bool IsValidISBN(string isbn)
        {
            isbn = isbn.Replace("-", "");
            switch (isbn.Length)
            {
                case 10:
                    return IsValidISBN10(isbn);
                    //break;
                case 13:
                    return IsValidISBN13(isbn);
                default:
                    return false;

            }

        }

        private bool IsValidISBN10(string isbn)
        {
            long sum = 0;
            if (isbn?.Length != 10 || 
                !long.TryParse(isbn?.Substring(0, 9), out long unused))
                return false;

            for (int i = 0; i<9; i++)
                sum += long.Parse(isbn[i].ToString()) * (10 - i);

            if (isbn[9] == 'X') sum += 10 * 10;
            else if (long.TryParse(isbn[9].ToString(), out unused)) sum += long.Parse(isbn[9].ToString());
            else return false;

            return sum % 11 == 0;
        }

        private bool IsValidISBN13(string isbn)
        {
            long sum = 0;
            int lastnum;

            if (isbn?.Length != 13 ||
            !long.TryParse(isbn?.Substring(0, 12), out long unused))
                return false;
            for (int i = 0; i < 12; i++)
            {
                int mult = i % 2 == 0 ? 1 : 3;
                sum += long.Parse(isbn[i].ToString()) * mult;
            }
            if (sum % 10 == 0)
                lastnum = 0;
            else
                lastnum = 10 - (int)(sum % 10);

            return lastnum == int.Parse(isbn[12].ToString());
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
            get { return _author.Entity; }
            set { _author.Entity = value; }
        }




        private EntityRef<Book> _book = new EntityRef<Book>();

        [Association(Name = "ISBNToBook", IsForeignKey = true, Storage = "_book", ThisKey = "ISBN")]
        public Book book
        {
            get { return _book.Entity; }
            set { _book.Entity = value; }
        }
    }
}
