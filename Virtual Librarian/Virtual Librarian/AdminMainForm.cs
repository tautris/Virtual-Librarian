using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Virtual_Librarian
{
    public partial class AdminMainForm : Form
    {
        public AdminMainForm()
        {
            InitializeComponent();
            AdminLoginTextBox.Select();
        }

        string LoginName;
        string Password;
        Admin currentAdmin;
        int selectedUserIndex = -1;
        int lastIndex = -1;
 

        private void AdminLoginTextBox_TextChanged(object sender, EventArgs e)
        {
            LoginName = AdminLoginTextBox.Text;
        }

        private void AdminPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            Password = AdminPasswordTextBox.Text;
        }

        private void AdminLoginButton_Click(object sender, EventArgs e)
        {
            FileReaderWriter fileReaderWriter = FileReaderWriter.Instance;
            List<Admin> admins = fileReaderWriter.GetAdmins();
            foreach (Admin admin in admins)
            {
                if (LoginName == admin.LoginName && Password == admin.Password)
                {
                    currentAdmin = admin;
                    LoginAdmin();
                   
                }

            }
        }

        private void LoginAdmin()
        {
            AdminLoginTextBox.Hide();
            AdminPasswordTextBox.Hide();
            AdminLoginButton.Hide();

            AdminUsersLabel.Show();
            AdminUsersListView.Show();
            AdminAddUserButton.Show();
            AdminRemoveUserButton.Show();

            List<User> users = currentAdmin.GetAllManagedUsers();
            InitListView();
            foreach (User user in users)
            {
                var item = new ListViewItem(new[] { user.Id.ToString(), user.Name, user.Surname });
                AdminUsersListView.Items.Add(item);
                lastIndex++;
            }
        }

        private void InitListView()
        {
            AdminUsersListView.View = View.Details;
            AdminUsersListView.Columns.Add("Id", 50, HorizontalAlignment.Left);
            AdminUsersListView.Columns.Add("Name", 150, HorizontalAlignment.Left);
            AdminUsersListView.Columns.Add("Surname", 150, HorizontalAlignment.Left);
        }

        private void AdminAddUserButton_Click(object sender, EventArgs e)
        {
            List<User> users = currentAdmin.GetAllManagedUsers();
            if(users.Count < 10)
            {
                if (!AddUserIdTextBox.Visible)
                {
                    AddUserIdTextBox.Show();
                    AddUserNameTextBox.Show();
                    AddUserSurnameTextBox.Show();
                    AddUserFacultyTextBox.Show();
                }
                else
                {
                    
                    int Id = Int32.Parse(AddUserIdTextBox.Text);
                    string Name = AddUserNameTextBox.Text;
                    string Surname = AddUserSurnameTextBox.Text;
                    User.Faculty faculty = 0;
                    try
                    {
                        Enum.TryParse(AddUserFacultyTextBox.Text, out faculty);
                    }
                    catch
                    {
                        MessageBox.Show("Faculty does not exist", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (Id != 0 && Name != null && Surname != null && Enum.IsDefined(typeof (User.Faculty), faculty))
                    {
                        User newUser = new User(Id, Name, Surname, faculty);
                        currentAdmin.AddManagedUser(index: lastIndex + 1, user: newUser);

                        var item = new ListViewItem(new[] { newUser.Id.ToString(), newUser.Name, newUser.Surname });
                        AdminUsersListView.Items.Add(item);
                        lastIndex++;

                        FileReaderWriter fileReaderWriter = FileReaderWriter.Instance;
                        fileReaderWriter.UpdateAdminUsers(currentAdmin);
                        fileReaderWriter.InsertUser(newUser);

                        AddUserIdTextBox.Hide();
                        AddUserNameTextBox.Hide();
                        AddUserSurnameTextBox.Hide();
                        AddUserFacultyTextBox.Hide();
                    }
                }

            }

        }

        private void AdminRemoveUserButton_Click(object sender, EventArgs e)
        {
            if(selectedUserIndex > -1)
            {
                currentAdmin.RemoveManagedUser(selectedUserIndex);
                AdminUsersListView.Items.RemoveAt(selectedUserIndex);
                selectedUserIndex = -1;
                lastIndex--;

                FileReaderWriter fileReaderWriter = FileReaderWriter.Instance;
                fileReaderWriter.UpdateAdminUsers(currentAdmin);
            }

        }

        private void AdminUsersListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.AdminUsersListView.SelectedItems.Count == 0)
                return;

            selectedUserIndex = AdminUsersListView.SelectedItems[0].Index;
           
        }
    }
}
