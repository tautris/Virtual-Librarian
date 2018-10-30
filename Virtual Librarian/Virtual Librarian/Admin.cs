using System.Collections.Generic;

namespace Virtual_Librarian
{
    public class Admin
    {

        //standard property
        private string _loginName;
        public string LoginName
        {
            get { return _loginName; }
            set { _loginName = value; }
        }

        //auto-implemented property
        public string Password { get; set; }


        //indexed property
        private User[] ManagedUsers = new User[10];
        public User this[int i]
        {
            get { return ManagedUsers[i]; }
            set { ManagedUsers[i] = value; }
        }

        //Named and optional arguments
        public void AddManagedUser(User user, int index = 0)
        {
            this[index] = user;
        }

        public User GetManagedUser(int index)
        {
            return this[index];
        }

        public void RemoveManagedUser(int index)
        {
            for (int i = index; i < 10; i++)
            {
                if (i != 9)
                {
                    this[i] = this[i + 1];
                }
                else
                {
                    this[i] = null;
                }
            }
        }

        public List<User> GetAllManagedUsers()
        {
            List<User> UserList = new List<User>();
            for(int i = 0; i < 10; i++)
            {
                User TempUser = GetManagedUser(i);
                if (TempUser != null)
                {
                    UserList.Add(TempUser);
                }
            }
            return UserList;
        }

        
        public Admin(string LoginName, string Password)
        {
            this.LoginName = LoginName;
            this.Password = Password;
        }
    }
}
