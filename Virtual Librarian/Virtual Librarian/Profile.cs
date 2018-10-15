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
    public partial class Profile : Form
    {
        public Profile()
        {
            InitializeComponent();


            AvailableBooks();
        }

        private void AvailableBooks()
        {
            for(int i=0; i<5; i++)
            {
                listView1.Items.Add(new ListViewItem(new string[] { "pavadinimas2 ", "laisva" }));
         
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainForm f2 = new MainForm();
            f2.Show();
            this.Hide();
        }
    }
}
