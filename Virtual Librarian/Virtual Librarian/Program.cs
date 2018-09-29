using System;
using System.Windows.Forms;
using System.Data.Linq;
using System.Data.Linq.Mapping;


namespace Virtual_Librarian
{
    static class Program
    {
        [STAThread]
        static void Main()
        {


           
            var user1 = new User("Tautvydas", "Dirmeikis", 170000, User.Faculty.MIF);
            LibraryDB db = new LibraryDB();
            db.testFunc();
            for (; ; );

            //To launch windows form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
