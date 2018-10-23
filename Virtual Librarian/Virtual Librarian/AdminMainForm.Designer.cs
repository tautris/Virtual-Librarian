namespace Virtual_Librarian
{
    partial class AdminMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AdminLoginButton = new System.Windows.Forms.Button();
            this.AdminLoginTextBox = new System.Windows.Forms.TextBox();
            this.AdminPasswordTextBox = new System.Windows.Forms.TextBox();
            this.AdminUsersLabel = new System.Windows.Forms.Label();
            this.AdminUsersListView = new System.Windows.Forms.ListView();
            this.AdminAddUserButton = new System.Windows.Forms.Button();
            this.AdminRemoveUserButton = new System.Windows.Forms.Button();
            this.AddUserIdTextBox = new System.Windows.Forms.TextBox();
            this.AddUserNameTextBox = new System.Windows.Forms.TextBox();
            this.AddUserSurnameTextBox = new System.Windows.Forms.TextBox();
            this.AddUserFacultyTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // AdminLoginButton
            // 
            this.AdminLoginButton.Location = new System.Drawing.Point(375, 258);
            this.AdminLoginButton.Name = "AdminLoginButton";
            this.AdminLoginButton.Size = new System.Drawing.Size(75, 23);
            this.AdminLoginButton.TabIndex = 2;
            this.AdminLoginButton.Text = "Login";
            this.AdminLoginButton.UseVisualStyleBackColor = true;
            this.AdminLoginButton.Click += new System.EventHandler(this.AdminLoginButton_Click);
            // 
            // AdminLoginTextBox
            // 
            this.AdminLoginTextBox.Location = new System.Drawing.Point(315, 149);
            this.AdminLoginTextBox.Name = "AdminLoginTextBox";
            this.AdminLoginTextBox.Size = new System.Drawing.Size(182, 22);
            this.AdminLoginTextBox.TabIndex = 3;
            this.AdminLoginTextBox.TextChanged += new System.EventHandler(this.AdminLoginTextBox_TextChanged);
            // 
            // AdminPasswordTextBox
            // 
            this.AdminPasswordTextBox.Location = new System.Drawing.Point(315, 207);
            this.AdminPasswordTextBox.Name = "AdminPasswordTextBox";
            this.AdminPasswordTextBox.Size = new System.Drawing.Size(182, 22);
            this.AdminPasswordTextBox.TabIndex = 4;
            this.AdminPasswordTextBox.UseSystemPasswordChar = true;
            this.AdminPasswordTextBox.TextChanged += new System.EventHandler(this.AdminPasswordTextBox_TextChanged);
            // 
            // AdminUsersLabel
            // 
            this.AdminUsersLabel.AutoSize = true;
            this.AdminUsersLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(253)))));
            this.AdminUsersLabel.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.AdminUsersLabel.Location = new System.Drawing.Point(13, 29);
            this.AdminUsersLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AdminUsersLabel.Name = "AdminUsersLabel";
            this.AdminUsersLabel.Size = new System.Drawing.Size(474, 39);
            this.AdminUsersLabel.TabIndex = 5;
            this.AdminUsersLabel.Text = "Users Managed By This Admin";
            this.AdminUsersLabel.Visible = false;
            // 
            // AdminUsersListView
            // 
            this.AdminUsersListView.Location = new System.Drawing.Point(7, 103);
            this.AdminUsersListView.Name = "AdminUsersListView";
            this.AdminUsersListView.Size = new System.Drawing.Size(480, 306);
            this.AdminUsersListView.TabIndex = 6;
            this.AdminUsersListView.UseCompatibleStateImageBehavior = false;
            this.AdminUsersListView.Visible = false;
            this.AdminUsersListView.SelectedIndexChanged += new System.EventHandler(this.AdminUsersListView_SelectedIndexChanged);
            // 
            // AdminAddUserButton
            // 
            this.AdminAddUserButton.Location = new System.Drawing.Point(596, 324);
            this.AdminAddUserButton.Name = "AdminAddUserButton";
            this.AdminAddUserButton.Size = new System.Drawing.Size(107, 31);
            this.AdminAddUserButton.TabIndex = 7;
            this.AdminAddUserButton.Text = "Add User";
            this.AdminAddUserButton.UseVisualStyleBackColor = true;
            this.AdminAddUserButton.Visible = false;
            this.AdminAddUserButton.Click += new System.EventHandler(this.AdminAddUserButton_Click);
            // 
            // AdminRemoveUserButton
            // 
            this.AdminRemoveUserButton.Location = new System.Drawing.Point(596, 375);
            this.AdminRemoveUserButton.Name = "AdminRemoveUserButton";
            this.AdminRemoveUserButton.Size = new System.Drawing.Size(107, 34);
            this.AdminRemoveUserButton.TabIndex = 8;
            this.AdminRemoveUserButton.Text = "Remove User";
            this.AdminRemoveUserButton.UseVisualStyleBackColor = true;
            this.AdminRemoveUserButton.Visible = false;
            this.AdminRemoveUserButton.Click += new System.EventHandler(this.AdminRemoveUserButton_Click);
            // 
            // AddUserIdTextBox
            // 
            this.AddUserIdTextBox.Location = new System.Drawing.Point(596, 135);
            this.AddUserIdTextBox.Name = "AddUserIdTextBox";
            this.AddUserIdTextBox.Size = new System.Drawing.Size(107, 22);
            this.AddUserIdTextBox.TabIndex = 9;
            this.AddUserIdTextBox.Visible = false;
            // 
            // AddUserNameTextBox
            // 
            this.AddUserNameTextBox.Location = new System.Drawing.Point(596, 178);
            this.AddUserNameTextBox.Name = "AddUserNameTextBox";
            this.AddUserNameTextBox.Size = new System.Drawing.Size(107, 22);
            this.AddUserNameTextBox.TabIndex = 10;
            this.AddUserNameTextBox.Visible = false;
            // 
            // AddUserSurnameTextBox
            // 
            this.AddUserSurnameTextBox.Location = new System.Drawing.Point(596, 223);
            this.AddUserSurnameTextBox.Name = "AddUserSurnameTextBox";
            this.AddUserSurnameTextBox.Size = new System.Drawing.Size(107, 22);
            this.AddUserSurnameTextBox.TabIndex = 11;
            this.AddUserSurnameTextBox.Visible = false;
            // 
            // AddUserFacultyTextBox
            // 
            this.AddUserFacultyTextBox.Location = new System.Drawing.Point(596, 259);
            this.AddUserFacultyTextBox.Name = "AddUserFacultyTextBox";
            this.AddUserFacultyTextBox.Size = new System.Drawing.Size(107, 22);
            this.AddUserFacultyTextBox.TabIndex = 12;
            this.AddUserFacultyTextBox.Visible = false;
            // 
            // AdminMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.AddUserFacultyTextBox);
            this.Controls.Add(this.AddUserSurnameTextBox);
            this.Controls.Add(this.AddUserNameTextBox);
            this.Controls.Add(this.AddUserIdTextBox);
            this.Controls.Add(this.AdminRemoveUserButton);
            this.Controls.Add(this.AdminAddUserButton);
            this.Controls.Add(this.AdminUsersListView);
            this.Controls.Add(this.AdminUsersLabel);
            this.Controls.Add(this.AdminPasswordTextBox);
            this.Controls.Add(this.AdminLoginTextBox);
            this.Controls.Add(this.AdminLoginButton);
            this.Name = "AdminMainForm";
            this.Text = "AdminMainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AdminLoginButton;
        private System.Windows.Forms.TextBox AdminLoginTextBox;
        private System.Windows.Forms.TextBox AdminPasswordTextBox;
        private System.Windows.Forms.Label AdminUsersLabel;
        private System.Windows.Forms.ListView AdminUsersListView;
        private System.Windows.Forms.Button AdminAddUserButton;
        private System.Windows.Forms.Button AdminRemoveUserButton;
        private System.Windows.Forms.TextBox AddUserIdTextBox;
        private System.Windows.Forms.TextBox AddUserNameTextBox;
        private System.Windows.Forms.TextBox AddUserSurnameTextBox;
        private System.Windows.Forms.TextBox AddUserFacultyTextBox;
    }
}