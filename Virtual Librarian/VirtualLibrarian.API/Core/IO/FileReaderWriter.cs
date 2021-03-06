﻿//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Text;
//using System.Web;
//using VirtualLibrarian.API.Core.Utilities;
//using VirtualLibrarian.Domain;

//namespace VirtualLibrarian.API.Core
//{

//    public class FileReaderWriter : IReaderWriter

//    {
//        private static readonly string projectDir = HttpContext.Current.Server.MapPath("~");
//        private static readonly string userFilePath = projectDir + @"\FilesIO\user.txt";
//        private static readonly string bookCopyFilePath = projectDir + @"\FilesIO\bookCopies.txt";
//        private static readonly string bookFilePath = projectDir + @"\FilesIO\books.txt";
//        private static readonly string adminsFilePath = projectDir + @"\FilesIO\admins.txt";

//        public void WriteLineToFile(string path, string data)
//        {
//            // TODO handle writing to file properly/ code refactor
//            if (!File.Exists(path))
//            {
//                try
//                {
//                    File.Create(path);
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine("The file could not be created:");
//                    Console.WriteLine(e.Message);
//                }

//                try
//                {
//                    using (TextWriter tw = new StreamWriter(path))
//                    {
//                        tw.WriteLine(data);
//                        tw.Close();
//                    }
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine("Error while trying to write to file:");
//                    Console.WriteLine(e.Message);
//                }
//            }
//            else if (File.Exists(path))
//            {
//                try
//                {
//                    using (var tw = new StreamWriter(path, true))
//                    {
//                        tw.WriteLine(data);
//                    }
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine("Error while trying to write to file:");
//                    Console.WriteLine(e.Message);
//                }
//            }
//        }

//        public void WriteLineToFileFixed(string path, string data)
//        {
//            if (!File.Exists(path))
//            {
//                try
//                {
//                    File.Create(path);
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine("The file could not be created:");
//                    Console.WriteLine(e.Message);
//                }

//                try
//                {
//                    using (TextWriter tw = new StreamWriter(path))
//                    {
//                        tw.Write("\r\n" + data);
//                        tw.Close();
//                    }
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine("Error while trying to write to file:");
//                    Console.WriteLine(e.Message);
//                }
//            }
//            else if (File.Exists(path))
//            {
//                try
//                {
//                    using (var tw = new StreamWriter(path, true))
//                    {
//                        tw.Write("\r\n" + data);
//                    }
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine("Error while trying to write to file:");
//                    Console.WriteLine(e.Message);
//                }
//            }
//        }

//        public string ReadFile(string path)
//        {
//            // TODO handle reading from file properly, exception handling
//            try
//            {
//                using (StreamReader sr = new StreamReader(path))
//                {
//                    string line = sr.ReadToEnd();
//                    return line;
//                }
//            }
//            catch(FileNotFoundException)
//            {
//                System.Diagnostics.Debug.WriteLine("File at path: " + path + " was not found");
//                return "Error";
//            }
//            catch (PathTooLongException)
//            {
//                System.Diagnostics.Debug.WriteLine("File path: " + path + " is too long");
//                return "Error";
//            }
//            catch (OutOfMemoryException)
//            {
//                System.Diagnostics.Debug.WriteLine("File contents were too big for string type variable");
//                return "Error";
//            }
//            catch (IOException)
//            {
//                System.Diagnostics.Debug.WriteLine("There was a prblem when trying to read file contents");
//                return "Error";
//            }
//        }

//        public User ParseUser(string userFileContent, int id)
//        {
//            string userEntry = userFileContent.FromToNewline(id.ToString());
//            string[] userProperties = userEntry.Split(',');

//            return new User(
//                id: int.Parse(userProperties[0]),
//                name: userProperties[1],
//                surname: userProperties[2],
//                faculty: (User.Faculty)Enum.Parse(typeof(User.Faculty), userProperties[3]));
//        }


//        public User GetUserFixed(int id)
//        {
//            string userFileContent = ReadFile(userFilePath);
//            string[] userList = userFileContent.Split('\n');
//            foreach (string user in userList)
//            {
//                string[] userProperties = user.Split(',');
//                if (userProperties[0] == id.ToString())
//                {
//                    return new User(id: int.Parse(userProperties[0]),
//                        name: userProperties[1], surname: userProperties[2],
//                        faculty: (User.Faculty)Enum.Parse(typeof(User.Faculty),
//                        userProperties[3]));
//                }
//            }
//            return null;
//        }


//        public User GetUser(int id)

//        {
//            string userFileContent = ReadFile(userFilePath);
//            return ParseUser(userFileContent, id);
//        }

//        public List<User> ParseUsers(string userFileContent)
//        {
//            string[] userEntries = userFileContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);
//            List<User> users = new List<User>();
//            foreach (string entry in userEntries)
//            {
//                string[] userProperties = entry.Split(',');
//                users.Add(new User(
//                    int.Parse(userProperties[0]),
//                    userProperties[1],
//                    userProperties[2],
//                    (User.Faculty)Enum.Parse(typeof(User.Faculty), userProperties[3])
//                ));
//            }
//            return users;
//        }
//        public List<User> GetUsers()
//        {
//            string userFileContent = ReadFile(userFilePath);
//            return ParseUsers(userFileContent);
//        }

//        public void InsertUser(User user)
//        {
//            WriteLineToFileFixed(userFilePath, user.Id + "," + user.Name + "," + user.Surname + "," + user.CurrentFaculty);
//        }

//        public Book GetBook(String ISBN)
//        {
//            string bookFileContent = ReadFile(bookFilePath);
//            string bookEntry = bookFileContent.FromToNewline(ISBN);
//            string[] bookProperties = bookEntry.Split(',');

//            //DateTime date = DateTime.ParseExact(bookProperties[4], "yyyy-MM-dd", CultureInfo.InvariantCulture);

//            return new Book(bookProperties[0], bookProperties[1], bookProperties[2], bookProperties[3], bookProperties[4], bookProperties[4]);
//        }

//        public List<Book> GetBooks()
//        {
//            List<Book> books = new List<Book>();
//            string bookFileContent = ReadFile(bookFilePath);
//            string[] bookEntries = bookFileContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);
//            foreach (string entry in bookEntries)
//            {
//                string[] bookProperties = entry.Split(',');

//                //DateTime date = DateTime.ParseExact(bookProperties[4], "yyyy-MM-dd", CultureInfo.InvariantCulture);
//                books.Add(new Book(bookProperties[0], bookProperties[1], bookProperties[2], bookProperties[3], bookProperties[4], bookProperties[5]));
//            }
//            return books;
//        }

//        public void InsertBook(Book book)          //TODO: Check for repeating ISBN when adding
//        {
//            WriteLineToFile(bookFilePath, book.ISBN + "," + book.title + "," + book.author + "," + book.description + "," + book.date.ToString("yyyy-MM-dd"));
//        }

//        public BookCopy GetBookCopy(int id)
//        {
//            string bookCopyFileContent = ReadFile(bookCopyFilePath);
//            string bookEntry = bookCopyFileContent.FromToNewline(id.ToString());
//            string[] bookCopyProperties = bookEntry.Split(',');

//            DateTime datePrinting = DateTime.ParseExact(bookCopyProperties[2], "yyyy-MM-dd", CultureInfo.InvariantCulture);
//            return new BookCopy(int.Parse(bookCopyProperties[0]), GetBook(bookCopyProperties[1]), datePrinting);
//        }

//        public List<BookCopy> GetBookCopies()
//        {
//            List<BookCopy> bookCopies = new List<BookCopy>();
//            string bookCopyFileContent = ReadFile(bookCopyFilePath);
//            string[] bookEntries = bookCopyFileContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);

//            foreach (string entry in bookEntries)
//            {
//                string[] bookCopyProperties = entry.Split(',');
//                DateTime datePrinting = DateTime.ParseExact(bookCopyProperties[2], "yyyy-MM-dd", CultureInfo.InvariantCulture);
//                bookCopies.Add(new BookCopy(int.Parse(bookCopyProperties[0]), GetBook(bookCopyProperties[1]), datePrinting));
//            }

//            return bookCopies;
//        }

//        public void InsertBookCopy(BookCopy bookCopy)
//        {
//            WriteLineToFile(bookCopyFilePath, bookCopy.Id + "," + bookCopy.book.ISBN + "," + bookCopy.dateOfPrinting.ToString("yyyy-MM-dd"));
//        }

//        public List<Admin> GetAdmins()
//        {
//            List<Admin> admins = new List<Admin>();
//            string adminFileContent = ReadFile(adminsFilePath);
//            if(adminFileContent == "Error")
//            {
//                adminFileContent = Retry<string>(ReadFile, adminsFilePath);
//                if(adminFileContent == "Error")
//                {
//                    return null;
//                }
//            }
//            string[] adminEntries = adminFileContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);
//            foreach (string entry in adminEntries)
//            {
//                //anonymous method
//                Func<char, string[]> anonMethod = delegate (char seperator)
//                {
//                    string[] anonAdminProperties = entry.Split(seperator);
//                    return anonAdminProperties;
//                };
//                string[] adminProperties = anonMethod(',');

//                Admin currentAdmin = new Admin(adminProperties[0], adminProperties[1]);
//                admins.Add(currentAdmin);
//                var usersList = new List<string>(adminProperties);
//                usersList.RemoveAt(0);
//                usersList.RemoveAt(0);
            
//                foreach (string userId in usersList)
//                {
//                    User currentUser = GetUserFixed(Int32.Parse(userId));
//                    currentAdmin.AddManagedUser(currentUser);
//                }
//            }
//            return admins;
//        }

//        public void UpdateAdminUsers(Admin admin)
//        {
//            StringBuilder sb = new StringBuilder();
//            string adminFileContent = ReadFile(adminsFilePath);
//            string[] adminEntries = adminFileContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);
//            foreach (string entry in adminEntries)
//            {
//                string[] adminProperties = entry.Split(',');
//                if (admin.LoginName != adminProperties[0])
//                {
//                    sb.Append(entry);
//                }

//            }

//            File.WriteAllText(adminsFilePath, sb.ToString());
//            sb.Clear();

//            sb.Append(admin.LoginName + "," + admin.Password);
//            foreach (User user in admin.GetAllManagedUsers())
//            {
//                sb.Append("," + user.Id.ToString());
//            }
//            WriteLineToFileFixed(adminsFilePath, sb.ToString());
//        }

//        public void RemoveUser(int id)
//        {
//            StringBuilder sb = new StringBuilder();
//            string userFileContent = ReadFile(userFilePath);
//            string[] userEntries = userFileContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);
//            foreach (string entry in userEntries)
//            {
//                string[] userProperties = entry.Split(',');
//                if (userProperties[0] != id.ToString())
//                {
//                    sb.Append(entry);
//                }

//            }
//            File.WriteAllText(userFilePath, sb.ToString());
//            sb.Clear();
//        }

//        //allows to retry calling method with generic return type and 1 generic argument 
//        public T Retry<T>(Func<T, T> func, T parameter)
//        {
//            return func(parameter);
//        }
//    }
//}
