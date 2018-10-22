﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtual_Librarian
{
    public class BookCopy
    {
        public DateTime dateOfPrinting { get; }
        //private DateTime dateOfPrinting { get; }
        private DateTime takenDate { get; set; }
        private DateTime? lastReturnDate { get; set; }
        public Book book { get; set; }
        public BookCopy (Book book, DateTime dateOfPrinting)
        {
            this.book = book;
            book.AddBookCopy(this);

            this.dateOfPrinting = dateOfPrinting;
            lastReturnDate = DateTime.Now;
            takenDate = DateTime.Now.AddSeconds(-1);
        }
        public void TakeCopy ()
        {
            if (IsAvailable())
            {
                takenDate = DateTime.Now;
                lastReturnDate = null;
            }
        }
        public void ReturnCopy ()
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
