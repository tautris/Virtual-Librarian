using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using System.Drawing;
using System.Windows.Forms;

namespace Virtual_Librarian
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var book1 = new Book ("978-3-16-148410-0", "Red Riding Hood", "John", "Smith", DateTime.Today.AddYears(-1));
            var bookIssue = new BookCopy(book1, DateTime.Today.AddMonths(-1));
            var user1 = new User("Tautvydas", "Dirmeikis", 170000, User.Faculty.MIF);
            user1.TakeBook(bookIssue);
            user1.printTakenBooks();

            //To launch windows form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
