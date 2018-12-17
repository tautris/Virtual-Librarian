using System.Collections.Generic;

namespace VirtualLibrarian.Domain.Models
{
    public class Admin : Base
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
        public ICollection<User> ManagedUsers { get; set; }
    }
}
