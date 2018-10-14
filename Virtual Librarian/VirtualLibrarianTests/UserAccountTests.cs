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
            Book book1 = new Book("123456789012", "Crime and Punishment", "Feodor", "Dostoyevsky", DateTime.Now.AddYears(-30));
            book1.addBookCopy(bookCopy1);

            var bookCount = Library.Instance.getAvailableBooksList().Count;
            var bookCopyCount = Library.Instance.getAllBookCopies().Count;

            Library.Instance.addBookCopy(bookCopy1, book1);

            Assert.AreEqual(bookCount + 1, Library.Instance.getAvailableBooksList().Count);//, 0, "The Book was not added successfully");
            Assert.AreEqual(bookCopyCount + 1, Library.Instance.getAllBookCopies().Count);//, 0, "The Book Copy was not added successfully");

            Library.Instance.removeBookCopy(bookCopy1, book1);
            Library.Instance.removeBook(book1);
        }

        [TestMethod]
        public void bookCopyRemoved()
        {
            BookCopy bookCopy1 = new BookCopy(DateTime.Now.AddMonths(-11));
            Book book1 = new Book("123456789012", "Crime and Punishment", "Feodor", "Dostoyevsky", DateTime.Now.AddYears(-30));
            book1.addBookCopy(bookCopy1);

            var bookCount = Library.Instance.getAllBooks().Count;
            var availableBookCount = Library.Instance.getAvailableBooksList().Count;
            var bookCopyCount = Library.Instance.getAllBookCopies().Count;

            Library.Instance.addBookCopy(bookCopy1, book1);
            Library.Instance.removeBookCopy(bookCopy1, book1);

            Assert.AreEqual(bookCount + 1, Library.Instance.getAllBooks().Count);
            Assert.AreEqual(availableBookCount, Library.Instance.getAvailableBooksList().Count);
            Assert.AreEqual(bookCopyCount, Library.Instance.getAllBookCopies().Count);

            Library.Instance.removeBook(book1);
        }

    }
}
