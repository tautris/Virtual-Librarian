﻿using System;
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
        List<Book> GetBooks();
        void InsertBook(Book book);
        BookCopy GetBookCopy(int id);
        List<BookCopy> GetBookCopies();
        void InsertBookCopy(BookCopy bookCopy);
    }
}
