using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARIAN2_Jegyfoglalo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (username.Text == "jegyvasarlo" && password.Text == "jegyvasarlo1" && type.SelectedItem.ToString()=="Jegyvásárló")
            {
                this.Hide();
                User u = new User();
                u.Show();
            }
            else if (username.Text == "rendszergazda" && password.Text == "rendszergazda1" && type.SelectedItem.ToString() == "Rendszergazda")
            {
                this.Hide();
                Admin a = new Admin();
                a.Show();
            }
            else
            {
                MessageBox.Show("Hiányzó vagy hibás bejelentkezési értékek!");
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
