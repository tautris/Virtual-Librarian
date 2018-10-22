using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtual_Librarian
{
    interface IReaderWriter
    {
        User GetUser(int id);
        List<User> GetUsers();
        void InsertUser(User user);
        Book GetBook(String ISBN);
        //BookCopy GetBookCopy(int id);

        // TODO implement some of the methods below in FileReaderWriter
        //BookCopy GetBookCopy(int id);
        //List<BookCopy> GetBookCopies(string ISBN);
        //void InsertBookCopy(BookCopy bookCopy);
        //Book GetBook(string ISBN);
        //List<Book> GetBooks();
        //void InsertBook(Book book);
    }
}
