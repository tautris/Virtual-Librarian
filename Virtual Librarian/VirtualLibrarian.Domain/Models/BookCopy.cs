using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualLibrarian.Domain.Models
{
    public class BookCopy : Base
    {
        public DateTimeOffset DateOfPrinting { get; }
        public DateTimeOffset TakenDate { get; set; }
        public DateTimeOffset ReturnDate { get; set; }
        public DateTimeOffset? LastReturnDate { get; set; }
        [ForeignKey("Book")]
        public Guid BookId { get; set; }
}
}
