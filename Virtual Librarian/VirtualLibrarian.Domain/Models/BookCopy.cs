using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualLibrarian.Domain.Models
{
    public class BookCopy : Base
    {
        public DateTime DateOfPrinting { get; set; }
        public DateTime? TakenDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? LastReturnDate { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
