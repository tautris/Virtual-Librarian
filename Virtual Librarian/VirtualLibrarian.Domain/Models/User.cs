using System.Collections.Generic;
using VirtualLibrarian.Domain.Enums;

namespace VirtualLibrarian.Domain.Models
{
    public class User : Base
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public FacultyEnum CurrentFaculty { get; set; }
        public ICollection<BookCopy> TakenBooks { get; set; }
    }
}
