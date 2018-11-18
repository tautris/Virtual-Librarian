using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualLibrarian.Domain;

namespace Virtual_Librarian
{
    //private BookService bookService;

    public partial class AvailableBooksForm : Form
    {
        private User user;
        static HttpClient client = new HttpClient();

        public AvailableBooksForm(User user)
        {
            this.user = user;
            InitializeComponent();

            AvailableBooks();
        }

        private async void AvailableBooks()
        {
            var bookTask = await client.GetAsync("http://localhost:50863/GetAvailableBooksSorted");
            //Getting available books list and printing titles
            List<Book> availableBooksList =
                JsonConvert.DeserializeObject<List<Book>>(await bookTask.Content.ReadAsStringAsync());
            foreach (Book book in availableBooksList)
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
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Book p = (Book)listView1.SelectedItems[0].Tag;
            MessageBox.Show(p.title, "Taken book:");
            user.TakeBook(p);

            //For testing what user is taking book
            //Console.WriteLine(user.Id + user.Name + user.Surname + user.CurrentFaculty);

            //Loads profile
            //new UserProfile(user).Show();
            this.Hide();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //new UserProfile(user).Show();
            this.Hide();
        }
    }
}
