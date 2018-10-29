using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Virtual_Librarian
{
    public partial class ReturnBookForm : Form
    {
        private User user;

        public ReturnBookForm(User user)
        {
            this.user = user;
            InitializeComponent();

            TakenBooks();
        }

        private void TakenBooks()
        {
            //Shows users taken books
            List<BookCopy> TakenBooksList = new List<BookCopy>(user.TakenBooks());
            foreach (BookCopy bookCopy in TakenBooksList)
            {
                ListViewItem item = new ListViewItem(new[] { bookCopy.book.title.ToString(), bookCopy.returnDate.ToString("m") });
                //item.Tag = bookCopy;
                listView1.Items.Add(item);
            }
        }

        //TODO return book
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            /*
            BookCopy p = (BookCopy)listView1.SelectedItems[0].Tag;
            //MessageBox.Show(p.title, "Returned book:");
            user.ReturnBook(p);

            new UserProfile(user).Show();
            this.Hide();
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new UserProfile(user).Show();
            this.Hide();
        }
    }
}
