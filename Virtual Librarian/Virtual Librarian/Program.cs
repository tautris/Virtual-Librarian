using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Virtual_Librarian
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            //Available books for testing list
            Book book1 = new Book("978-1-56619-909-4", "Test1", "test", "testy", DateTime.Now.AddYears(-30));
            BookCopy bookCopy1 = new BookCopy(DateTime.Now.AddMonths(-11));
            book1.AddBookCopy(bookCopy1);
            Library.Instance.AddBook(book1);
            Book book2 = new Book("978-1-56619-909-4", "TestBook2", "test", "test", DateTime.Now.AddYears(-30));
            BookCopy bookCopy2 = new BookCopy(DateTime.Now.AddMonths(-11));
            book2.AddBookCopy(bookCopy2);
            Library.Instance.AddBook(book2);
            //------------------------------------------------------------------

            //To launch windows form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }


    }
}
