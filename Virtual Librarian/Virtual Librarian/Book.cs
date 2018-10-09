using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtual_Librarian
{
    class Book
    {
        private string title { get; }
        private string ISBN { get; }
        private string authorName { get; }
        private string authorSurname { get; }
        private DateTime date { get; set; }

        public Book(string ISBN, string title, string authorName, string authorSurname, DateTime date)
        {
            this.title = title;
            this.authorName = authorName;
            this.authorSurname = authorSurname;
            this.date = date;
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
            return this.ISBN;
        }
    }
}
