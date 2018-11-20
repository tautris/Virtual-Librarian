using System.Collections.Generic;
using System.Linq;
using VirtualLibrarian.Domain;

namespace VirtualLibrarian.API.Core
{
    public class Library : ILibrary
    {
        private List<Book> allBooks = new List<Book>();
        private List<User> allUsers = new List<User>();
        private List<Admin> allAdmins = new List<Admin>();
        private readonly IReaderWriter _readerWriter;
        public Library(IReaderWriter readerWriter)
        {
            _readerWriter = readerWriter;

            allBooks = _readerWriter.GetBooks();
            allUsers = _readerWriter.GetUsers();
            allAdmins = _readerWriter.GetAdmins();
            List <BookCopy> bookCopies = new List<BookCopy>();
            bookCopies = _readerWriter.GetBookCopies();
            foreach (BookCopy bookCopy in bookCopies)
            {
                allBooks.First(obj => obj.ISBN == bookCopy.book.ISBN).AddBookCopy(bookCopy);
            }
            */
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
            List<Book> sortedAvailableBooks = availableBooks.OrderBy(o => o.author).ToList();
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
        public List<User> GetAllUsers()
        {
            return allUsers;
        }

        public Book GetBook(int id)
        {
            foreach(Book book in allBooks )
            {
                if (book.id == id)
                {
                    return book;
                }
            }
            return null;
        }

        public Book LikeBook(int id)
        {
            Book book = GetBook(id);
            if (book != null)
            {
                book.likes++;
                return book;
            }
            return null;
        }
        public void ReviewBook(Book book, string comment, double star)
        {
            book.ReviewBook(comment, star);
        }

    }
}