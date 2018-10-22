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
            //User user = FileReaderWriter.Instance.GetUser(1);
            //Console.WriteLine(user.Id + user.Name + user.Surname + user.CurrentFaculty);

            //var users = FileReaderWriter.Instance.GetUsers();
            //foreach (User u in users)
            //{
            //    Console.WriteLine(u.Id + u.Name + u.Surname + u.CurrentFaculty);

            //}
            //To launch windows form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
