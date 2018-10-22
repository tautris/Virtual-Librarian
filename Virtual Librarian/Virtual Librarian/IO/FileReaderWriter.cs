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

            return new User(int.Parse(userProperties[0]), userProperties[1], userProperties[2], (User.Faculty)Enum.Parse(typeof(User.Faculty), userProperties[3]));
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

            DateTime dateTime = DateTime.ParseExact(bookProperties[4], "yyyy-MM-dd", CultureInfo.InvariantCulture);

            return new Book(bookProperties[0], bookProperties[1], bookProperties[2], bookProperties[3], dateTime);
        }
    }
}
