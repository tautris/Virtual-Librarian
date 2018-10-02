using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Virtual_Librarian
{
    //[Database]
    internal class LibraryDataContext : DataContext
    {
        public Table<User>       Users;
        public Table<Book>       Books;
        public Table<BookCopy>   BookCopies;
        public Table<AuthorBook> BookAuthors;
        public Table<Author>     Authors;
        //public Table
        

        public LibraryDataContext(string connection) : base(connection) {}
    }
    class LibraryDB
    {
        //private const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\giriukaralius\source\repos\Virtual-Librarian\Virtual Librarian\Virtual Librarian\libraryDB.mdf;Integrated Security=True";
        private LibraryDataContext db { get; }


        public LibraryDB()
        {
            db = new LibraryDataContext(Properties.Settings.Default.libraryDBConnectionString);

        }

        public List<User> getUsersFromDB()
        {
            Table<User> users = db.Users;
            List<User> retlist = new List<User>();
            foreach (User user in users)
            {
                retlist.Add(user);
            }
            return retlist;
            
        }

        public void testFunc()
        {
            var a = db.GetTable<BookCopy>();
            foreach (var i in a)
                System.Console.WriteLine(i.ToString());
        }

    }
}
