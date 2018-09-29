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
    [Table(Name = "BOOKS")]
    class Book
    {
        [Column]
        public string  title { get; private set; }
        [Column(IsPrimaryKey =true)]
        public string  ISBN { get; private set; }

        public string  authorName { get; private set; }
        public string  authorSurname { get; }
        [Column]
        public  short   publishYear { get; private set; }
        [Column]
        public string  publisher   { get; private set; }

        public Book() {; } //needed for LINQ
        public Book (string  ISBN, string title, string authorName, string authorSurname, short publishYear)
        {
            this.title= title;
            this.authorName = authorName;
            this.authorSurname = authorSurname;
            this.publishYear = publishYear;
            if (!IsValidISBN(ISBN))
                throw new ArgumentException("ISBN is not valid");
            else
                this.ISBN = ISBN.Replace("-", "");

        }

        public override string ToString()
        {
            ISBN = ISBN.Length == 13 ? IsbnCode.Parse(ISBN).ToString(IsbnCodeStyles.WithHyphens) : IsbnCode.Parse(ISBN).ToString(IsbnCodeStyles.AsIsbn10Code | IsbnCodeStyles.WithHyphens );

            return String.Format("Title: {0}\nAuthor: {1}. {2}\nPublished: {3}\nISBN: {4}", title, authorName[0], authorSurname, publishYear, ISBN);
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
}
