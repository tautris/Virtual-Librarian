using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace Virtual_Librarian
{
    //[Database]
    internal class LibraryDataContext : DataContext
    {
        public Table<User> Users;
        public Table<Book> Books;
        public Table<BookCopy> BookCopies;
        public Table<AuthorBook> BookAuthors;
        public Table<Author> Authors;
        //public Table


        public LibraryDataContext(string connection) : base(connection) { }
    }
    class LibraryDB
    {
        //private const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\giriukaralius\source\repos\Virtual-Librarian\Virtual Librarian\Virtual Librarian\libraryDB.mdf;Integrated Security=True";
        private LibraryDataContext db { get; }


        public LibraryDB()
        {
            db = new LibraryDataContext(Properties.Settings.Default.libraryDBConnectionString);

        }

        public List<User> getUsers()
        {
            Table<User> users = db.Users;
            List<User> retlist = new List<User>();
            foreach (User user in users)
            {
                retlist.Add(user);
            }
            return retlist;

        }

        public List<Tuple<User, List<BookCopy>>> getUserWithTakenBooks()
        {
            var query = db.Users.GroupJoin(db.BookCopies,
                user => user.studentId,
                books => books.takenByID,
                (user, books) => new { user, books });
            //return new IEnumerable<Tuple<User, IEnumerable<BookCopy>>> ;
            List<Tuple<User, List<BookCopy>>> retval = new List<Tuple<User, List<BookCopy>>>();
            foreach (var user in query)
            {
                retval.Add(new Tuple<User, List<BookCopy>>(user.user, user.books.ToList()));
            }
            return retval;

        }

        public void testFunc()
        {
            Table<BookCopy> a = db.GetTable<BookCopy>();
            foreach (BookCopy i in a)
            {
                System.Console.WriteLine(i.ToString());
            }
        }
    }
}
