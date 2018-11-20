using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibrarian.Domain;

namespace VirtualLibrarian.API.Core
{
    public interface ILibrary
    {
        void AddBook(Book book);
        void AddBookCopy(BookCopy bookCopy, Book book);
        void RemoveBook(Book book);
        void RemoveBookCopy(BookCopy bookCopy, Book book);
        List<Book> GetAvailableBooksSorted();
        List<Book> GetAllBooks();
        List<BookCopy> GetAvailableBookCopies();
        List<User> GetAllUsers();
        List<Admin> GetAllAdmins();
        Book GetBook(int id);
        Book LikeBook(int id);
        void ReviewBook(Book book, string comment, double star);
    }
}
