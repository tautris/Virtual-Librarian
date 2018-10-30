using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Virtual_Librarian;

namespace VirtualLibrarianTests
{
    [TestClass]
    public class UserAccountTests
    {
        [TestMethod]
        public void bookCopyAdded()
        {   
            Book book1 = new Book("978-1-56619-909-4", "Crime and Punishment", "Feodor", "Dostoyevsky", DateTime.Now.AddYears(-30));
            BookCopy bookCopy1 = new BookCopy(1, book1, DateTime.Now.AddMonths(-11));

            var bookCount = Library.Instance.GetAvailableBooksList().Count;
            var bookCopyCount = Library.Instance.GetAllBookCopies().Count;

            Library.Instance.AddBookCopy(bookCopy1, bookCopy1.book);

            Assert.AreEqual(bookCount + 1, Library.Instance.GetAvailableBooksList().Count);
            Assert.AreEqual(bookCopyCount + 1, Library.Instance.GetAllBookCopies().Count);

            Library.Instance.RemoveBookCopy(bookCopy1, bookCopy1.book);
            Library.Instance.RemoveBook(book1);
        }

        [TestMethod]
        public void bookCopyRemoved()
        {
            Book book1 = new Book("978-1-56619-909-4", "Crime and Punishment", "Feodor", "Dostoyevsky", DateTime.Now.AddYears(-30));
            BookCopy bookCopy1 = new BookCopy(1, book1, DateTime.Now.AddMonths(-11));

            var bookCount = Library.Instance.GetAllBooks().Count;
            var availableBookCount = Library.Instance.GetAvailableBooksList().Count;
            var bookCopyCount = Library.Instance.GetAllBookCopies().Count;

            Library.Instance.AddBookCopy(bookCopy1, bookCopy1.book);
            Library.Instance.RemoveBookCopy(bookCopy1, bookCopy1.book);

            Assert.AreEqual(bookCount + 1, Library.Instance.GetAllBooks().Count);
            Assert.AreEqual(availableBookCount, Library.Instance.GetAvailableBooksList().Count);
            Assert.AreEqual(bookCopyCount, Library.Instance.GetAllBookCopies().Count);

            Library.Instance.RemoveBook(book1);
        }
        [TestMethod]
        public void takeBook()
        {
            User user = new User(1, "Test", "Testy");
            Book book1 = new Book("978-1-56619-909-4", "Crime and Punishment", "Feodor", "Dostoyevsky", DateTime.Now.AddYears(-30));
            BookCopy bookCopy1 = new BookCopy(1, book1, DateTime.Now.AddMonths(-11));

            var userBookCount = user.TakenBooks().Count;
          
            user.TakeBook(book1);

            Assert.AreEqual(userBookCount + 1, user.TakenBooks().Count);
           
            Library.Instance.RemoveBookCopy(bookCopy1, bookCopy1.book);
            Library.Instance.RemoveBook(book1);
            user = null;
        }

        [TestMethod]
        public void returnBook()
        {
            User user = new User(1, "Test", "Testy");
            Book book1 = new Book("978-1-56619-909-4", "Crime and Punishment", "Feodor", "Dostoyevsky", DateTime.Now.AddYears(-30));
            BookCopy bookCopy1 = new BookCopy(1, book1, DateTime.Now.AddMonths(-11));

            var userBookCount = user.TakenBooks().Count;

            user.TakeBook(book1);
            user.ReturnBook(user.TakenBooks()[0]);

            Assert.AreEqual(userBookCount , user.TakenBooks().Count);

            Library.Instance.RemoveBookCopy(bookCopy1, bookCopy1.book);
            Library.Instance.RemoveBook(book1);
            user = null;
        }

        [TestMethod]
        public void AddAndReadUser()
        {
            User user = new User(10, "testinis", "useris");
            FileReaderWriter fileReaderWriter = FileReaderWriter.Instance;
            fileReaderWriter.InsertUser(user);
            User newUser = fileReaderWriter.GetUserFixed(10);
    
            Assert.AreEqual(user.Id, newUser.Id);
            Assert.AreEqual(user.Name, newUser.Name);
            Assert.AreEqual(user.Surname, newUser.Surname);
            Assert.AreEqual(user.CurrentFaculty, newUser.CurrentFaculty);

            fileReaderWriter.RemoveUser(10);
        }

     }
}
