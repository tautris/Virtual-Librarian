using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Virtual_Librarian
{
     class LibraryDB
    {
        //private const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\libraryDB.mdf;Integrated Security=True";
        private DataContext db { get; }


        public LibraryDB()
        {
            db = new DataContext(Properties.Settings.Default.libraryDBConnectionString);

        }

        public List<User> getUsersFromDB()
        {
            Table<User> users = db.GetTable<User>();
            List<User> retlist = new List<User>();
            foreach (User user in users)
            {
                retlist.Add(user);
            }
            return retlist;
            
        }

        public void testFunc()
        {
            Table<User> Users = db.GetTable<User>();
            var q =
                from a in Users
                select a;
            foreach (var c in Users)
            {
                Console.WriteLine(c.name);
            }
        }

    }
}
