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
        public AvailableBooksForm()
        {
            InitializeComponent();

            AvailableBooks();
        }

        private void AvailableBooks()
        {
            //Getting available books list and printing titles
            List<Book> AvailableBooksList = new List<Book>(Library.Instance.GetAvailableBooksList());
            foreach (Book book in AvailableBooksList)
            {
                listView1.Items.Add(book.title.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        //After selecting book 
        //TO DO take the book that is selected
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Hide();
            ListView listView = (ListView)sender;
            MessageBox.Show(listView.SelectedItems[0].ToString(), "Selected book");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
