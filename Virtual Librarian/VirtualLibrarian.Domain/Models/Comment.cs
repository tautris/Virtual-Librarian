using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualLibrarian.Domain.Models
{
    public class Comment : Base
    {
        [ForeignKey("Book")]
        public Guid BookCopyId { get; set; }
        public string Text { get; set; }
    }
}
