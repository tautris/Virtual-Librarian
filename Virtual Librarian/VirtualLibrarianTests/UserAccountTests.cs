using System;
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
            BookCopy bookCopy1 = new BookCopy(DateTime.Now.AddMonths(-11));
            Book book1 = new Book("978-1-56619-909-4", "Crime and Punishment", "Feodor", "Dostoyevsky", DateTime.Now.AddYears(-30));
            book1.AddBookCopy(bookCopy1);

            var bookCount = Library.Instance.GetAvailableBooksList().Count;
            var bookCopyCount = Library.Instance.GetAllBookCopies().Count;

            Library.Instance.AddBookCopy(bookCopy1, book1);

            Assert.AreEqual(bookCount + 1, Library.Instance.GetAvailableBooksList().Count);//, 0, "The Book was not added successfully");
            Assert.AreEqual(bookCopyCount + 1, Library.Instance.GetAllBookCopies().Count);//, 0, "The Book Copy was not added successfully");

            Library.Instance.RemoveBookCopy(bookCopy1, book1);
            Library.Instance.RemoveBook(book1);
        }

        [TestMethod]
        public void bookCopyRemoved()
        {
            BookCopy bookCopy1 = new BookCopy(DateTime.Now.AddMonths(-11));
            Book book1 = new Book("978-1-56619-909-4", "Crime and Punishment", "Feodor", "Dostoyevsky", DateTime.Now.AddYears(-30));
            book1.AddBookCopy(bookCopy1);

            var bookCount = Library.Instance.GetAllBooks().Count;
            var availableBookCount = Library.Instance.GetAvailableBooksList().Count;
            var bookCopyCount = Library.Instance.GetAllBookCopies().Count;

            Library.Instance.AddBookCopy(bookCopy1, book1);
            Library.Instance.RemoveBookCopy(bookCopy1, book1);

            Assert.AreEqual(bookCount + 1, Library.Instance.GetAllBooks().Count);
            Assert.AreEqual(availableBookCount, Library.Instance.GetAvailableBooksList().Count);
            Assert.AreEqual(bookCopyCount, Library.Instance.GetAllBookCopies().Count);

            Library.Instance.RemoveBook(book1);
        }

    }
}
