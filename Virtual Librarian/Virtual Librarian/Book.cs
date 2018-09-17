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

        public Book (string ISBN, string title, string authorName, string authorSurname, DateTime date)
        {
            this.title= title;
            this.ISBN = ISBN;
            this.authorName = authorName;
            this.authorSurname = authorSurname;
            this.date = date;
        }
        public override string ToString()
        {
            return String.Format("Title: {0}\nAuthor: {1}. {2}\nPublished: {3} {4}\nISBN: {5}", title, authorName[0], authorSurname, date.ToString("MMMM"), date.Year, ISBN);
        }
    }
}
