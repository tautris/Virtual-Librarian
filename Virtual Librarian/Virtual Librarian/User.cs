using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;


namespace Virtual_Librarian
{

    [Table(Name="USERS")]
    class User
    {
        public enum Faculty
        {
            MIF = 1,
            VM,
            MF,
            KF,
            GMC,
            FF,
            EVAF,
            CGF
        }

        [Column(CanBeNull =false)] public string name { get; private set; }

         
        [Column(CanBeNull = false)] public string surname { get; private set; }


        [Column(Name="faculty", CanBeNull =false, DbType = "INT")]
        public Faculty faculty { get; private set; }

        [Column(IsPrimaryKey =true, Name = "studentID")]
        public  int studentId { get; private set; }


        public List<BookCopy> takenBooks = new List<BookCopy>();

        public User() {; } //Required for SQL LINQ
        public User (string name, string surname, int studentId, Faculty faculty) {
            this.name = name;
            this.surname = surname;
            this.studentId = studentId;
            this.faculty = faculty;
        }
        /*
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
    */
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
