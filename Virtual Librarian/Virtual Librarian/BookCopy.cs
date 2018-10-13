using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtual_Librarian
{
    public class BookCopy
    {
        private DateTime dateOfPrinting { get; }
        public DateTime takenDate { get; set; }
        public DateTime? lastReturnDate { get; set; }
        public BookCopy (DateTime dateOfPrinting)
        {
            this.dateOfPrinting = dateOfPrinting;
        }
    }
}
