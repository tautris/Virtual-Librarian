using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtual_Librarian
{
    sealed class FileReaderWriter : IReaderWriter
    {
        private static readonly string projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        private static readonly string userFilePath = projectDir + @"\FilesIO\user.txt";
        private static readonly string bookCopyFilePath = projectDir + @"\FilesIO\bookCopies.txt";
        private static readonly string bookFilePath = projectDir + @"\FilesIO\books.txt";
        private static readonly string adminsFilePath = projectDir + @"\FilesIO\admins.txt";

        private static FileReaderWriter instance = null;
        private static readonly object padLock = new object();
        FileReaderWriter() { }
        public static FileReaderWriter Instance
        {
            get
            {
                lock (padLock)
                {
                    if (instance == null)
                    {
                        instance = new FileReaderWriter();
                    }
                    return instance;
                }
            }
        }

        public void WriteLineToFile(string path, string data)
        {
            // TODO handle writing to file properly/ code refactor
            if (!File.Exists(path))
            {
                try
                {
                    File.Create(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be created:");
                    Console.WriteLine(e.Message);
                }

                try
                {
                    using (TextWriter tw = new StreamWriter(path))
                    {
                        tw.WriteLine(data);
                        tw.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while trying to write to file:");
                    Console.WriteLine(e.Message);
                }
            }
            else if (File.Exists(path))
            {
                try
                {
                    using (var tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine(data);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error while trying to write to file:");
                    Console.WriteLine(e.Message);
                }   
            }
        }

        public string ReadFile(string path)
        {
            // TODO handle reading from file properly, exception handling
            try
            { 
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = sr.ReadToEnd();
                    return line;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return "Error";
            }
        }

        public User GetUser(int id)
        {
            string userFileContent = ReadFile(userFilePath);
            string userEntry = userFileContent.FromToNewline(id.ToString());
            string[] userProperties = userEntry.Split(',');

            return new User(id: int.Parse(userProperties[0]), 
                name: userProperties[1], surname: userProperties[2],
                faculty: (User.Faculty)Enum.Parse(typeof(User.Faculty), 
                userProperties[3]));
        }

        public User GetUserFixed(int id)
        {
            string userFileContent = ReadFile(userFilePath);
            string[] userList = userFileContent.Split('\n');
            foreach (string user in userList)
            {
                string[] userProperties = user.Split(',');
                if (userProperties[0] == id.ToString())
                {
                    return new User(id: int.Parse(userProperties[0]),
                        name: userProperties[1], surname: userProperties[2],
                        faculty: (User.Faculty)Enum.Parse(typeof(User.Faculty),
                        userProperties[3]));
                }
            }
            return null;
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            string userFileContent = ReadFile(userFilePath);
            string[] userEntries = userFileContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (string entry in userEntries)
            {
                string[] userProperties = entry.Split(',');
                users.Add(new User(int.Parse(userProperties[0]), userProperties[1], userProperties[2], (User.Faculty)Enum.Parse(typeof(User.Faculty), userProperties[3])));
            }

            return users;
        }

        public void InsertUser(User user)
        {
            WriteLineToFile(userFilePath, user.Id + "," + user.Name + "," + user.Surname + "," + user.CurrentFaculty);
        }

        public Book GetBook(String ISBN)
        {
            string bookFileContent = ReadFile(bookFilePath);
            string bookEntry = bookFileContent.FromToNewline(ISBN);
            string[] bookProperties = bookEntry.Split(',');

            DateTime date = DateTime.ParseExact(bookProperties[4], "yyyy-MM-dd", CultureInfo.InvariantCulture);

            return new Book(bookProperties[0], bookProperties[1], bookProperties[2], bookProperties[3], date);
        }

        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();
            string bookFileContent = ReadFile(bookFilePath);
            string[] bookEntries = bookFileContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (string entry in bookEntries)
            {
                string[] bookProperties = entry.Split(',');

                DateTime date = DateTime.ParseExact(bookProperties[4], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                books.Add(new Book(bookProperties[0], bookProperties[1], bookProperties[2], bookProperties[3], date));
            }
            return books;
        }

        public void InsertBook (Book book)          //TODO: Check for repeating ISBN when adding
        {       
            WriteLineToFile(bookFilePath, book.ISBN + "," + book.title + "," + book.authorName + "," + book.authorSurname + "," + book.date.ToString("yyyy-MM-dd"));
        }

        public BookCopy GetBookCopy(int id)
        {
            string bookCopyFileContent = ReadFile(bookCopyFilePath);
            string bookEntry = bookCopyFileContent.FromToNewline(id.ToString());
            string[] bookCopyProperties = bookEntry.Split(',');

            DateTime datePrinting = DateTime.ParseExact(bookCopyProperties[2], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            return new BookCopy(int.Parse(bookCopyProperties[0]), GetBook(bookCopyProperties[1]), datePrinting);
        }

        public List<BookCopy> GetBookCopies()
        {
            List<BookCopy> bookCopies = new List<BookCopy>();
            string bookCopyFileContent = ReadFile(bookCopyFilePath);
            string[] bookEntries = bookCopyFileContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            foreach (string entry in bookEntries)
            {
                string[] bookCopyProperties = entry.Split(',');
                DateTime datePrinting = DateTime.ParseExact(bookCopyProperties[2], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                bookCopies.Add(new BookCopy(int.Parse(bookCopyProperties[0]), GetBook(bookCopyProperties[1]), datePrinting));
            }
        
            return bookCopies;
        }

        public void InsertBookCopy(BookCopy bookCopy)
        {
            WriteLineToFile(bookCopyFilePath, bookCopy.Id + "," + bookCopy.book.ISBN + "," + bookCopy.dateOfPrinting.ToString("yyyy-MM-dd"));
        }

        public List<Admin> GetAdmins()
        {
            List<Admin> admins = new List<Admin>();
            string adminFileContent = ReadFile(adminsFilePath);
            string[] adminEntries = adminFileContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (string entry in adminEntries)
            {
                string[] adminProperties = entry.Split(',');
                Admin currentAdmin = new Admin(adminProperties[0], adminProperties[1]);
                admins.Add(currentAdmin);
                var usersList = new List<string>(adminProperties);
                usersList.RemoveAt(0);
                usersList.RemoveAt(0);
                int userIndex = 0;
                foreach(string userId in usersList)
                {
                    User currentUser = GetUserFixed(Int32.Parse(userId));
                    currentAdmin.AddManagedUser(currentUser, userIndex);
                    userIndex++;
                }
            }
            return admins;
        }

        public void UpdateAdminUsers(Admin admin)
        {
            StringBuilder sb = new StringBuilder();
            string adminFileContent = ReadFile(adminsFilePath);
            string[] adminEntries = adminFileContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (string entry in adminEntries)
            {
                string[] adminProperties = entry.Split(',');
                if (admin.LoginName != adminProperties[0])
                {
                    sb.Append(entry);
                }

            }

            File.WriteAllText(adminsFilePath, sb.ToString());
            sb.Clear();      
                        
            sb.Append(admin.LoginName + "," + admin.Password);
            foreach(User user in admin.GetAllManagedUsers())
            {
                sb.Append("," + user.Id.ToString());
            }
            WriteLineToFile(adminsFilePath, sb.ToString());
        }
    }
}
