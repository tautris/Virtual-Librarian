using System.Collections.Generic;
using System.Linq;
using VirtualLibrarian.Domain;

namespace VirtualLibrarian.API.Core
{
    public sealed class Library
    {
        private static Library instance = null;
        private static readonly object padLock = new object();
        private List<Book> allBooks = new List<Book>();
        private List<User> allUsers = new List<User>();
        private List<Admin> allAdmins = new List<Admin>();

        Library()
        {
            allBooks = FileReaderWriter.Instance.GetBooks();
            allUsers = FileReaderWriter.Instance.GetUsers();
            allAdmins = FileReaderWriter.Instance.GetAdmins();
            List <BookCopy> bookCopies = new List<BookCopy>();
            bookCopies = FileReaderWriter.Instance.GetBookCopies();
            foreach (BookCopy bookCopy in bookCopies)
            {
                allBooks.First(obj => obj.ISBN == bookCopy.book.ISBN).AddBookCopy(bookCopy);
            }
        }
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

        public void AddBook(Book book)
        {
            allBooks.Add(book);
        }
        public void AddBookCopy(BookCopy bookCopy, Book book)
        {
            if (!allBooks.Contains(book))
            {
                AddBook(book);
            }
            book.AddBookCopy(bookCopy);
        }
        public void RemoveBook(Book book)
        {
            allBooks.Remove(book);
        }
        public void RemoveBookCopy(BookCopy bookCopy, Book book)
        {
            book.RemoveBookCopy(bookCopy);
        }
        public List<Book> GetAvailableBooksSorted()
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
            List<Book> sortedAvailableBooks = availableBooks.OrderBy(o => o.authorSurname).ToList();
            return sortedAvailableBooks;
        }

        public List<Book> GetAllBooks()
        {
            return allBooks;
        }
        public List<BookCopy> GetAvailableBookCopies()
        {
            List<BookCopy> availableBookCopies = new List<BookCopy>();
            List<Book> availableBooks = new List<Book>();
            availableBooks = GetAvailableBooksSorted();
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

        public List<Admin> GetAllAdmins()
        {
            return allAdmins;
        }
    }
}