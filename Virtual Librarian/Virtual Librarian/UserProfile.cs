using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualLibrarian.Domain;

namespace Virtual_Librarian
{
    public partial class UserProfile : Form
    {
        private User user;

        public UserProfile(User user)
        {
            this.user = user;
            InitializeComponent();
            listView1.View = View.Details;

            textBox1.AppendText(user.Name);
            TakenBooks();   
        }

        private void TakenBooks()
        {
            //Shows users taken books
            List<BookCopy> TakenBooksList = new List<BookCopy>(user.TakenBooks());
            foreach (BookCopy bookCopy in TakenBooksList)
            {
                ListViewItem item = new ListViewItem(new[] { bookCopy.book.title.ToString(), bookCopy.returnDate.ToString("m")});
                //item.Tag = bookCopy;
                listView1.Items.Add(item);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void logOff_Click(object sender, EventArgs e)
        {
            new MainForm().Show();
            this.Hide();

        }

        private void takeBook_Click(object sender, EventArgs e)
        {
            new AvailableBooksForm(user).Show();
            this.Hide();
        }

        //TODO return book
        private void returnBook_Click(object sender, EventArgs e)
        {
            new ReturnBookForm(user).Show();
            this.Hide();
        }
    }
}
