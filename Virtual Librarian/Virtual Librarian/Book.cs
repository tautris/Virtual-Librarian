using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtual_Librarian
{
    public class Book
    {
        //public string title;
        public string title { get; }
        public string ISBN { get; }
        public string authorName { get; }
        public string authorSurname { get; }
        public DateTime date { get; set; }
        public List<BookCopy> copies = new List<BookCopy>();
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
        public void AddBookCopy (BookCopy copy)
        {
            if (!copies.Contains(copy))
            {
                copies.Add(copy);
            }
        }
        public void RemoveBookCopy (BookCopy copy)
        {
            copies.Remove(copy);
        }

        public override string ToString()
        {
            return this.ISBN;
        }
    }
}
