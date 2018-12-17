using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using VirtualLibrarian.Domain.Enums;
using VirtualLibrarian.Domain.Models;

namespace VirtualLibrarian.API.Core.Context
{
    public class LibraryContextInitializer : SqliteDropCreateDatabaseAlways<LibraryContext>
    {
        public LibraryContextInitializer(DbModelBuilder modelBuilder) : base(modelBuilder) { }

        protected override void Seed(LibraryContext context)
        {
            var users = new List<User>
            {
                new User()
                {
                    Id = 1,
                    Name = "Adomas",
                    //Surname = "Adomenas",
                    CurrentFaculty = FacultyEnum.CGF,
                },
                new User()
                {
                    Id = 2,
                    Name = "Tomas",
                    //Surname = "Tomenas",
                    CurrentFaculty = FacultyEnum.MIF
                },
                new User()
                {
                    Id = 3,
                    Name = "Barbora",
                    //Surname = "Barboraite",
                    CurrentFaculty = FacultyEnum.MF
                },
            };

            var bookCopies = new List<BookCopy>
            {
                new BookCopy()
                {
                    Id = 1,
                    BookId = 1,
                    UserId = 1,
                    DateOfPrinting = DateTime.Now.AddYears(-20),
                    TakenDate = DateTime.Now,
                    LastReturnDate = DateTime.Now,
                },
                new BookCopy()
                {
                    Id = 2,
                    BookId = 2,
                    UserId = 3,
                    DateOfPrinting = DateTime.Now.AddYears(-30),
                    LastReturnDate = DateTime.Now,
                },
                new BookCopy()
                {
                    Id = 3,
                    BookId = 1,
                    DateOfPrinting = DateTime.Now.AddYears(-25),
                },
                new BookCopy()
                {
                    Id = 4,
                    BookId = 3,
                    DateOfPrinting = DateTime.Now.AddYears(-10),
                },
            };

            var books = new List<Book>
            {
                new Book()
                {
                    Id = 1,
                    ISBN = "978-0486454115",
                    Author = "Feodor Dostoyevsky",
                    Title = "Crime and Punishment",
                    Pdf = "http://www.planetpdf.com/planetpdf/pdfs/free_ebooks/Crime_and_Punishment_NT.pdf",
                    Image = "https://upload.wikimedia.org/wikipedia/en/4/4b/Crimeandpunishmentcover.png",
                    Likes = 0,
                },
                new Book()
                {
                    Id = 2,
                    ISBN = "978-0679734529",
                    Author = "George Orwell",
                    Title = "1984",
                    Pdf = "https://www.planetebook.com/free-ebooks/1984.pdf",
                    Image = "https://upload.wikimedia.org/wikipedia/en/c/c3/1984first.jpg",
                    Likes = 0
                },
                new Book()
                {
                    Id = 3,
                    ISBN = "978-8026874256",
                    Author = "George Orwell",
                    Title = "1984",
                    Pdf = "https://www.planetebook.com/free-ebooks/1984.pdf",
                    Image = "https://upload.wikimedia.org/wikipedia/en/c/c3/1984first.jpg",
                    Likes = 0,
                },
            };


            context.Set<User>().AddRange(users);
            context.Set<Book>().AddRange(books);
            context.Set<BookCopy>().AddRange(bookCopies);

            context.SaveChanges();
        }
    }
}