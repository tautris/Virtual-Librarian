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
        [Column]
        public int copyID { get; private set; }
        [Column]
        public DateTime takenDate { get;  set; }
        [Column]
        public DateTime? lastReturnDate { get; set; }
        [Association(ThisKey = "ISBN")]
        private Book book { get; }


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
