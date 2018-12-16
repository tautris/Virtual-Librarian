using System.ComponentModel.DataAnnotations.Schema;
using VirtualLibrarian.Domain.Enums;

namespace VirtualLibrarian.Domain.Models
{
    public class User : Base
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public FacultyEnum CurrentFaculty { get; set; }
        [ForeignKey("Admin")]
        public int? AdminId { get; set; }
        public virtual Admin Admin { get; set; }
    }
}
