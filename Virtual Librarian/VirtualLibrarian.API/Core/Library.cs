using System.Collections.Generic;
using System.Linq;
using VirtualLibrarian.Domain.Models;

namespace VirtualLibrarian.API.Core
{
    public class Library : ILibraryManager
    {
        private readonly IReaderWriter _readerWriter;

        public Library(IReaderWriter readerWriter)
        {
            _readerWriter = readerWriter;
        }

        public ICollection<Book> GetAvailableBooksSorted()
        {
            var allBooks = _readerWriter.GetBooks();

            ICollection<Book> availableBooks = new List<Book>();
            foreach (Book book in allBooks)
            {
                foreach (BookCopy bookCopy in book.Copies)
                {
                    if (bookCopy.LastReturnDate != null && !availableBooks.Contains(book))
                    {
                        availableBooks.Add(book);
                    }
                }
            }
            ICollection<Book> sortedAvailableBooks = availableBooks.OrderBy(o => o.Author).ToList();
            return sortedAvailableBooks;
        }

        public ICollection<Book> GetAllBooks()
        {
            return _readerWriter.GetBooks();
        }

        public ICollection<BookCopy> GetAvailableBookCopies()
        {
            ICollection<BookCopy> availableBookCopies = new List<BookCopy>();
            ICollection<Book> availableBooks = new List<Book>();
            availableBooks = GetAvailableBooksSorted();
            foreach (Book book in availableBooks)
            {
                foreach (BookCopy bookCopy in book.Copies)
                {
                    if (bookCopy.LastReturnDate != null)
                    {
                        availableBookCopies.Add(bookCopy);
                    }
                }
            }
            return availableBookCopies;
        }

        public ICollection<User> GetAllUsers()
        {
            return _readerWriter.GetUsers();
        }

        public int GetUserId(string userName)
        {
            ICollection<User> allUsers = new List<User>();
            allUsers = GetAllUsers();
            foreach (User user in allUsers)
            {
                if(user.Name==userName)
                {
                    return user.Id;
                }
            }
            return -1;
        }

        public Book GetBook(int id)
        {
            var book = _readerWriter.GetBook(id);
            if(book == null)
            {
                //lool
                return null;
            }
            return book;
        }

        public Book LikeBook(int id)
        {
            var book = _readerWriter.GetBook(id);
            if(book != null)
            {
                _readerWriter.LikeBook(id);
                return book;

            }
            return null;
        }

        //    public void ReviewBook(Book book, string comment, double star)
        //    {
        //        book.ReviewBook(comment, star);
        //    }
        //    public List<Admin> GetAllAdmins()
        //    {
        //        return allAdmins;
        //    }
        //    public void AddBook(Book book)
        //    {
        //        allBooks.Add(book);
        //    }
        //    public void AddBookCopy(BookCopy bookCopy, Book book)
        //    {
        //        if (!allBooks.Contains(book))
        //        {
        //            AddBook(book);
        //        }
        //        book.AddBookCopy(bookCopy);
        //    }
        //    public void RemoveBook(Book book)
        //    {
        //        allBooks.Remove(book);
        //    }
        //    public void RemoveBookCopy(BookCopy bookCopy, Book book)
        //    {
        //        book.RemoveBookCopy(bookCopy);
        //    }
    }
}