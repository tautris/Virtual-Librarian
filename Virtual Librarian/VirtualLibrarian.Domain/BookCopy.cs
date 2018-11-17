using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibrarian.Domain
{
    public class BookCopy
    {
        public DateTime dateOfPrinting { get; }
        //private DateTime dateOfPrinting { get; }
        private DateTime takenDate { get; set; }
        public DateTime returnDate { get; set; }
        public DateTime? lastReturnDate { get; set; }
        public Book book { get; set; }
        public int Id { get; set; }
        public BookCopy(int id, Book book, DateTime dateOfPrinting)
        {
            this.book = book;
            book.AddBookCopy(this);

            this.Id = id;
            this.dateOfPrinting = dateOfPrinting;
            lastReturnDate = DateTime.Now;
            takenDate = DateTime.Now.AddSeconds(-1);
        }
        public void TakeCopy()
        {
            if (IsAvailable())
            {
                takenDate = DateTime.Now;
                returnDate = takenDate.AddMonths(1);
                lastReturnDate = null;
            }
        }
        public void ReturnCopy()
        {
            if (!IsAvailable())
            {
                lastReturnDate = DateTime.Now;
            }
        }
        public bool IsAvailable()
        {
            if (lastReturnDate != null)
                return true;
            else
                return false;
        }
    }
}

