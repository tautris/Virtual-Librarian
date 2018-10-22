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

            var x = FileReaderWriter.Instance.GetBook("978-0679734529");

            //Available books for testing list
            Book book1 = new Book("978-1-56619-909-4", "Test1", "test", "testy", DateTime.Now.AddYears(-30));
            BookCopy bookCopy1 = new BookCopy(book1, DateTime.Now.AddMonths(-11));
            Library.Instance.AddBook(book1);
            Book book2 = new Book("978-1-56619-909-4", "TestBook2", "test", "test", DateTime.Now.AddYears(-30));
            BookCopy bookCopy2 = new BookCopy(book2, DateTime.Now.AddMonths(-11));
            Library.Instance.AddBook(book2);
            //------------------------------------------------------------------
            //User user = FileReaderWriter.Instance.GetUser(1);
            //Console.WriteLine(user.Id + user.Name + user.Surname + user.CurrentFaculty);

            //var users = FileReaderWriter.Instance.GetUsers();
            //foreach (User u in users)
            //{
            //    Console.WriteLine(u.Id + u.Name + u.Surname + u.CurrentFaculty);

            //}
            //To launch windows form
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());
        }
    }
}
