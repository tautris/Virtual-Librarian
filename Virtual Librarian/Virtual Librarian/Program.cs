using System;
using System.Windows.Forms;


namespace Virtual_Librarian
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            //To launch windows form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
