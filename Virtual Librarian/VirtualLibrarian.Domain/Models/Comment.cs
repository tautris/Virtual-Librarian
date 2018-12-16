using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualLibrarian.Domain.Models
{
    public class Comment : Base
    {
        [ForeignKey("BookCopy")]
        public int BookCopyId { get; set; }
        public virtual BookCopy BookCopy { get; set; }
        public string Text { get; set; }
    }
}
