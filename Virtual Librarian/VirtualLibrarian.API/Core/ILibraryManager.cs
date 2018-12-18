using System.Collections.Generic;
using VirtualLibrarian.Domain.Models;

namespace VirtualLibrarian.API.Core
{
    public interface ILibraryManager
    {
        ICollection<Book> GetAvailableBooksSorted();
        ICollection<Book> GetAllBooks();
        ICollection<BookCopy> GetAvailableBookCopies();
        ICollection<User> GetAllUsers();
        Book GetBook(int id);
        Book LikeBook(int id);
        int GetUserId(string userName);
        //void ReviewBook(Book book, string comment, double star);
        //void AddBook(Book book);
        //void AddBookCopy(BookCopy bookCopy, Book book);
        //void RemoveBook(Book book);
        //void RemoveBookCopy(BookCopy bookCopy, Book book);
        //ICollection<Admin> GetAllAdmins();
    }
}
