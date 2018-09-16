using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtual_Librarian
{
    class User
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
        private string name { get; }
        private string surname { get; }
        private Faculty faculty { get; set; }
        private string studentId { get; }
        public List<BookCopy> takenBooks = new List<BookCopy>();
        public User (string name, string surname, string studentId, Faculty faculty) {
            this.name = name;
            this.surname = surname;
            this.studentId = studentId;
            this.faculty = faculty;
        }

        public void TakeBook (BookCopy bookCopy)
        {
            takenBooks.Add(bookCopy);
            bookCopy.takenDate = DateTime.Now;
            bookCopy.lastReturnDate = null;
        }

        public void ReturnBook (BookCopy bookCopy)
        {
            takenBooks.Remove(bookCopy);
            bookCopy.lastReturnDate = DateTime.Now;
        }
  
        public List<BookCopy> TakenBooks ()
        {
            return takenBooks;
        }

        public void printTakenBooks () //Temporary, just for testing purposes
        {
            takenBooks.ForEach(Console.WriteLine);
        }
    }
}
