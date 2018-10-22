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
    //private BookService bookService;

    public partial class AvailableBooksForm : Form
    {
        private User user;

        public AvailableBooksForm(User user)
        {
            this.user = user;
            InitializeComponent();

            AvailableBooks();
        }

        private void AvailableBooks()
        {
            //Getting available books list and printing titles
            List<Book> AvailableBooksList = new List<Book>(Library.Instance.GetAvailableBooksList());
            foreach (Book book in AvailableBooksList)
            {
                ListViewItem item = new ListViewItem();
                item.Text = book.title.ToString();
                item.Tag = book;
                listView1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        //After selecting book 
        //TO DO take the book that is selected
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Book p = (Book)listView1.SelectedItems[0].Tag;
            MessageBox.Show(p.title, "Taken book:");
            user.TakeBook(p);

            //For testing what user is taking book
            //Console.WriteLine(user.Id + user.Name + user.Surname + user.CurrentFaculty);
            
            //Loads form with less availabke books
            AvailableBooksForm f2 = new AvailableBooksForm(user);
            f2.Show();
            this.Hide();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
