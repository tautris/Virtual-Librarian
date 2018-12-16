using SQLite.CodeFirst;
using System.Data.Entity;
using VirtualLibrarian.Domain.Models;

namespace VirtualLibrarian.API.Core.Context
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("libraryDb") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new LibraryContextInitializer(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
