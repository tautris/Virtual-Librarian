using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtual_Librarian
{
    public sealed class Library
    {
        private static Library instance = null;
        private static readonly object padLock = new object();
        Library() { }
        public static Library Instance
        {
            get
            {
                lock (padLock)
                {
                    if (instance == null)
                    {
                        instance = new Library();
                    }
                    return instance;
                }
            }
        }
        private List<Book> allBooks = new List<Book>();
        private List<BookCopy> allBookCopies = new List<BookCopy>();
        public void addBook(Book book)
        {
            allBooks.Add(book);
        }
        public void addBookCopy(BookCopy bookCopy, Book book)
        {
            if (!allBooks.Contains(book))
            {
                addBook(book);
            }
            book.addBookCopy(bookCopy);
        }
        public void removeBook(Book book)
        {
            allBooks.Remove(book);
        }
        public void removeBookCopy(BookCopy bookCopy, Book book)
        {
            book.removeBookCopy(bookCopy);
        }
        public List<Book> getAvailableBooksList()
        {
            List<Book> availableBooks = new List<Book>();
            foreach (Book book in allBooks) {

            }
            return availableBooks;
        }
    }
}
