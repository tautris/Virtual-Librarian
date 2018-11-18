using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibrarian.Domain
{
    public class Book
    {
        //public string title;
        public int id { get; set; }
        public string author { get; }
        public string title { get; }
        public string description { get; }
        public int likes { get; set; }
        public string pdf { get; set; }
        public string image { get; set; }
        public string ISBN { get; }
        public DateTime date { get; set; }
        public List<BookCopy> copies = new List<BookCopy>();

        public Book(string ISBN, string title, string author, string id, string pdf, string image)
        {
            this.title = title;
            this.author = author;
            description = "very nice";
            this.id = Int32.Parse(id);
            likes = 1;
            this.pdf = pdf;
            this.image = image;
            if (!Validators.IsValidISBN(ISBN))
            {
                throw new ArgumentException("ISBN is not valid");
            }
            else
            {
                this.ISBN = ISBN.Replace("-", "");
            }
        }
        public void AddBookCopy(BookCopy copy)
        {
            if (!copies.Contains(copy))
            {
                copies.Add(copy);
            }
        }
        public void RemoveBookCopy(BookCopy copy)
        {
            copies.Remove(copy);
        }

        public override string ToString()
        {
            return this.ISBN;
        }
    }
}
