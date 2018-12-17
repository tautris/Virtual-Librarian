using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibrarian.Domain;

namespace VirtualLibrarian.Domain
{
    public class User
    {
        public enum Faculty
        {
            MIF,
            VM,
            MF,
            KF,
            GMC,
            FF,
            EVAF,
            CGF
        }
        public string Name { get; }
        //public string Surname { get; }
        public Faculty CurrentFaculty { get; set; }
        public int Id { get; }
        public List<BookCopy> takenBooks = new List<BookCopy>();
        //public User(int id, string name, string surname, Faculty faculty = Faculty.MIF)
        public User(int id, string name, Faculty faculty = Faculty.MIF)
        {
            Id = id;
            Name = name;
            //Surname = surname;
            CurrentFaculty = faculty;
        }

        public void TakeBook(Book book)
        {
            foreach (BookCopy bookCopy in book.copies)
            {
                if (bookCopy.IsAvailable())
                {
                    TakeBookCopy(bookCopy);
                    return;
                }
            }
        }
        public void TakeBookCopy(BookCopy bookCopy)
        {
            takenBooks.Add(bookCopy);
            bookCopy.TakeCopy();
        }

        public void ReturnBook(BookCopy bookCopy)
        {
            takenBooks.Remove(bookCopy);
            bookCopy.ReturnCopy();
        }

        public List<BookCopy> TakenBooks()
        {
            return takenBooks;
        }

        public void PrintTakenBooks() //Temporary, just for testing purposes
        {
            takenBooks.ForEach(Console.WriteLine);
        }
    }
}
