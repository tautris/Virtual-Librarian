using System.Collections.Generic;
using VirtualLibrarian.Domain.Models;

namespace VirtualLibrarian.API.Core
{
    public interface IReaderWriter
    {
        ICollection<User> GetUsers();
        Book GetBook(int id);
        ICollection<Book> GetBooks();
        ICollection<BookCopy> GetBookCopies();
        Book LikeBook(int id);
        //User GetUser(int id);
        //void InsertUser(User user);
        //void InsertBook(Book book);
        //BookCopy GetBookCopy(int id);
        //void InsertBookCopy(BookCopy bookCopy);
        //List<Admin> GetAdmins();
    }
}
