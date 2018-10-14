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
            foreach (Book book in allBooks)
            {
                foreach (BookCopy bookCopy in book.copies)
                {
                    if (bookCopy.IsAvailable() && !availableBooks.Contains(book))
                    {
                        availableBooks.Add(book);
                    }
                }
            }
            return availableBooks;
        }

        public List<Book> getAllBooks()    
        {
            return allBooks;
        }
        public List<BookCopy> getAllBookCopies()
        {
            List<BookCopy> availableBookCopies = new List<BookCopy>();
            List<Book> availableBooks = new List<Book>();
            availableBooks = getAvailableBooksList();
            foreach (Book book in availableBooks)
            {
                foreach (BookCopy bookCopy in book.copies)
                {
                    if (bookCopy.IsAvailable())
                    {
                        availableBookCopies.Add(bookCopy);
                    }
                }
            }
            return availableBookCopies;
        }
    }
}
