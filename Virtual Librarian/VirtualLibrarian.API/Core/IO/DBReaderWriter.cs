using System;
using System.Collections.Generic;
using System.Linq;
using VirtualLibrarian.API.Core.Context;
using VirtualLibrarian.Domain.Models;

namespace VirtualLibrarian.API.Core.IO
{
    public class DBReaderWriter : IReaderWriter
    {
        private readonly LibraryContext _context;
        public DBReaderWriter(LibraryContext context)
        {
            _context = context;
        }

        public ICollection<User> GetUsers()
        {
            var query = from u in _context.Users
                        orderby u.Name
                        select u;
            return query.ToList();
        }

        public ICollection<Book> GetBooks()
        {
            var books = _context.Books.ToList();
            return books;
        }

        public ICollection<BookCopy> GetBookCopies()
        {
            var bookCopies = _context.BookCopies.ToList();
            return bookCopies;
        }

        public Book GetBook(int id)
        {
            var book = _context.Books.Where(b => b.Id == id).First();
            return book;
        }

        public Book LikeBook(int id)
        {
            var book = _context.Books.Where(b => b.Id == id).First();
            //update statement could be used here and query executed as indexer get users
            book.Likes++;
            _context.SaveChanges();

            return book;
        }
        //public void InsertUser(User user)
        //{

        //}

        //public List<Book> GetBooks()
        //{

        //}

        //public void InsertBook(Book book)
        //{

        //}

        //public BookCopy GetBookCopy(int id)
        //{

        //}

        //public void InsertBookCopy(BookCopy bookCopy)
        //{

        //}
        //public List<Admin> GetAdmins()
        //{

        //}
    }
}