using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Virtual_Librarian
{
    [Table(Name = "BookCopies")]
    class BookCopy
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)] public int copyID { get; private set; }
        [Column(CanBeNull = true)]  public DateTime? takenDate      { get;  set; }
        [Column(CanBeNull = true)]  public DateTime? lastReturnDate { get;  set; }
        [Column(CanBeNull = true)]  public DateTime? returnUntil    { get;  set; }
        [Column(CanBeNull = false)] private string ISBN { get; set; }
        [Column(CanBeNull = true, Name = "takenBy")]  private int? takenByID { get; set; }
        
        private EntityRef<Book> _book    = new EntityRef<Book>();
        private EntityRef<User> _takenBy = new EntityRef<User>();

        [Association(Name = "CopyToBook", IsForeignKey = true, Storage = "_book", ThisKey = "ISBN")]
        public Book book
        {
            get { return _book.Entity; }
            private set { _book.Entity = value; }
        }

        [Association(Name = "CopyToStudent", Storage = "_takenBy", ThisKey = "takenByID", IsForeignKey = true)]
        public User takenBy
        {
            get { return _takenBy.Entity; }
            private set { _takenBy.Entity = value;  }
        }

   

        public BookCopy() { }
        public BookCopy (Book book)
        {
            this.book = book;
        }
        public override string ToString()
        {
            return book.ToString();
        }
    }
}
