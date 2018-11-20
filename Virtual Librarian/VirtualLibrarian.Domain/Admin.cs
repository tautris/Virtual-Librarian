using System.Collections.Generic;

namespace VirtualLibrarian.Domain
{
    public class Admin
    {

        public string LoginName { get; set; }
        public string Password { get; set; }
        public List<User> ManagedUsers { get; set; }


        public void AddManagedUser(User newUser)
        {
            if (!ManagedUsers.Contains(newUser))
            {
                ManagedUsers.Add(newUser);
            }
        }    

        public void RemoveManagedUser(User user)
        {
            ManagedUsers.Remove(user);
        }

        public List<User> GetAllManagedUsers()
        {
            return ManagedUsers;
        }


        public Admin(string LoginName, string Password)
        {
            this.LoginName = LoginName;
            this.Password = Password;
            this.ManagedUsers = new List<User>();
        }
    }
}
