using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Virtual_Librarian
{
    [Table(Name = "BookCopies")]
    class BookCopy
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)] public int copyID { get; private set; }
        [Column(CanBeNull = true)] public DateTime? takenDate { get; set; }
        [Column(CanBeNull = true)] public DateTime? lastReturnDate { get; set; }
        [Column(CanBeNull = true)] public DateTime? returnUntil { get; set; }
        [Column(CanBeNull = false)] public string ISBN { get; private set; }  //private because it's set by LINQ  and should not be set manually
                                                                              //it's needed as it's a foreign key      
        [Column(CanBeNull = true, Name = "takenBy")] public int? takenByID { get; private set; } //same

        private EntityRef<Book> _book = new EntityRef<Book>();
        private EntityRef<User> _takenBy = new EntityRef<User>();

        [Association(Name = "CopyToBook", IsForeignKey = true, Storage = "_book", ThisKey = "ISBN")]
        public Book book
        {
            get => _book.Entity;
            private set => _book.Entity = value;
        }

        [Association(Name = "CopyToStudent", Storage = "_takenBy", ThisKey = "takenByID", IsForeignKey = true)]
        public User takenBy
        {
            get => _takenBy.Entity;
            private set => _takenBy.Entity = value;
        }



        public BookCopy() { }
        public BookCopy(Book book)
        {
            this.book = book;
        }
        public override string ToString()
        {
            return book.ToString();
        }
    }
}
