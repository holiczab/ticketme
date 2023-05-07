using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace ARIAN2_Jegyfoglalo
{
    public partial class User : Form
    {
        DB db = new DB();
        SQLiteDataAdapter sda;
        DataTable dt;
        SQLiteCommand cmd;
        int mode;
        int eredeti_ar;
        int[,] ulesek = new int[3,16];
        CheckBox[,] seats;
        int jegyszam1=0, jegyszam2=0;
        double kedvezmeny;
        public User()
        {
            InitializeComponent();
            seats = new CheckBox[,] 
            {{a1,a2,a3,a4,a5,a6,a7,a8, aa1, aa2, aa3, aa4, aa5, aa6, aa7, aa8 },
            { b1,b2,b3,b4,b5,b6,b7,b8, bb1, bb2, bb3, bb4, bb5, bb6, bb7, bb8 } ,
            { c1,c2,c3,c4,c5,c6,c7,c8, cc1, cc2, cc3, cc4, cc5, cc6, cc7, cc8 } };
        }
        public void koronalizalo()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (ulesek[i, j] == 2)
                    {
                        if (i + 1 < 3 && ulesek[i + 1, j]!=2) ulesek[i + 1, j] = 1;
                        if (i - 1 > -1 && ulesek[i - 1, j] != 2) ulesek[i - 1, j] = 1;
                        if (j + 1 < 16 && ulesek[i, j + 1] != 2) ulesek[i, j + 1] = 1;
                        if (j - 1 > -1 && ulesek[i, j - 1] != 2) ulesek[i, j - 1] = 1;
                    }
                }
            }
            for (int i = 0; i < ulesek.GetLength(0); i++)
            {
                for (int j = 0; j < ulesek.GetLength(1); j++)
                {
                    Console.Write(ulesek[i, j] + "\t");
                }
                Console.WriteLine();
            }
            db.openconnection();
            string comm = "update ulesek set";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    comm += " "+seats[i, j].Name + "=" + ulesek[i,j]+",";
                }
            }
            cmd = new SQLiteCommand(comm.Substring(0,comm.Length-1)+" where Menetrend='" + utvonal.Text + "'", db.GetConnection());
            sda = new SQLiteDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            db.closeconnection();

        }
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 lo = new Form1();
            lo.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tol.Items.Clear();
            tol.Text = "";
            ig.Items.Clear();
            ig.Text = "";
            nem.Text = "Belföldi";
            mode = 0;
            string[] megallok = { "Veszprém", "Székesfehérvár", "Budapest",
                "Szeged", "Hódmezővásárhely", "Szentes", "Kecskemét"};
            for (int i = 0; i < megallok.Length; i++)
            {
                tol.Items.Add(megallok[i]);
            }
            ig.Enabled = false;
            Tól.Text = "Honnan";
            label26.Text = "Hova";
            label25.Text = "Vásárló neve:";
            label28.Text = "Jegyszám:";
            label27.Text = "Kupon:";
            label30.Text = "Végösszeg:";
            button1.Text = "Vásárlás";
            button1.Enabled = false;
            panel1.Visible = true;
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Készítette: Holicza Barnabás");
        }

        private void hToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tol.Items.Clear();
            tol.Text = "";
            ig.Items.Clear();
            ig.Text = "";
            nem.Text = "International";
            mode = 1;
            string[] megallok = { "Budapest", "Pozsony", "Prága", "Drezda", "Berlin", "Hamburg"
            , "Bécs", "Klagenfurt", "Velence", "Róma"};
            for (int i = 0; i < megallok.Length; i++)
            {
                tol.Items.Add(megallok[i]);
            }
            ig.Enabled = false;
            Tól.Text = "From";
            label26.Text = "To";
            label25.Text = "Name:";
            label28.Text = "Number of tickets:";
            label27.Text = "Coupon:";
            label30.Text = "Total:";
            button1.Text = "Buy";
            button1.Enabled = false;
            panel1.Visible = true;
        }

        private void User_Load(object sender, EventArgs e)
        {
            nem.Text = "";
            utvonal.Text = "";
            button1.Enabled = false;
            MessageBox.Show("Kérem válasszon nyelvet a menetrendek megtekintéséhez!\n" +
                "Please choose a language to see the available railway guides!");
        }

        private void tol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(mode==1)
            {
                ig.Items.Clear();
                ig.Enabled = true;
                string[] megallok = { "Budapest", "Pozsony", "Prága", "Drezda", "Berlin", "Hamburg"
            , "Bécs", "Klagenfurt", "Velence", "Róma"};
                for (int i = 0; i < megallok.Length; i++)
                {
                    if (tol.SelectedItem.ToString() != megallok[i]) ig.Items.Add(megallok[i]);
                }
            }
            else 
            {
                ig.Items.Clear();
                ig.Enabled = true;
                string[] megallok = {"Veszprém", "Székesfehérvár", "Budapest", 
                "Szeged", "Hódmezővásárhely", "Szentes", "Kecskemét"};
                for (int i = 0; i < megallok.Length; i++)
                {
                    if (tol.SelectedItem.ToString() != megallok[i]) ig.Items.Add(megallok[i]);
                }
            }
            button1.Enabled = false;

        }

        private void User_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void ig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mode == 1)
            {
                string[] Budapest_Hamburg = { "Budapest", "Pozsony", "Prága", "Drezda", "Berlin", "Hamburg" };
                string[] Bécs_Róma = { "Bécs", "Klagenfurt", "Velence", "Róma" };
                if (Budapest_Hamburg.Contains(tol.SelectedItem.ToString()) &&
                    Budapest_Hamburg.Contains(ig.SelectedItem.ToString()))
                {
                    utvonal.Text = "Budapest-Hamburg";
                    feltolto();
                }
                else if (Bécs_Róma.Contains(tol.SelectedItem.ToString()) &&
                    Bécs_Róma.Contains(ig.SelectedItem.ToString()))
                {
                    utvonal.Text = "Bécs-Róma";
                    feltolto();
                }
                else
                {
                    utvonal.Text = "Nincs ilyen útvonal!";
                    button1.Enabled = false;
                    panel1.Visible = true;
                }
            }
            else
            {
                string[] Veszprém_Budapest = { "Veszprém", "Székesfehérvár", "Budapest" };
                string[] Szeged_Kecskemét = { "Szeged", "Hódmezővásárhely", "Szentes", "Kecskemét" };
                if (Veszprém_Budapest.Contains(tol.SelectedItem.ToString()) &&
                    Veszprém_Budapest.Contains(ig.SelectedItem.ToString()))
                {
                    utvonal.Text = "Veszprém-Budapest";
                    feltolto();
                }
                else if (Szeged_Kecskemét.Contains(tol.SelectedItem.ToString()) &&
                    Szeged_Kecskemét.Contains(ig.SelectedItem.ToString()))
                {
                    utvonal.Text = "Szeged-Kecskemét";
                    feltolto();
                }
                else
                {
                    utvonal.Text = "Nincs ilyen útvonal!";
                    button1.Enabled = false;
                    panel1.Visible = true;
                }

            }
            
            
        }
        public void feltolto()
        {
            db.openconnection();
            cmd = new SQLiteCommand("select * from ulesek where Menetrend='" + utvonal.Text + "'", db.GetConnection());
            sda = new SQLiteDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            db.closeconnection();
            int a=1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    ulesek[i, j] = Convert.ToInt32(dt.Rows[0].ItemArray[a]);
                    a++;
                }
            }
            for (int i = 0; i < ulesek.GetLength(0); i++)
            {
                for (int j = 0; j < ulesek.GetLength(1); j++)
                {
                    Console.Write(ulesek[i, j] + "\t");
                }
                Console.WriteLine();
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    seats[i, j].Enabled = true;
                    seats[i, j].Checked = false;
                    seats[i, j].BackColor = Color.White;
                    if (ulesek[i, j] == 1) seats[i, j].Enabled = false;
                    else if (ulesek[i, j] == 2)
                    {
                        seats[i, j].Enabled = false;
                        seats[i, j].BackColor = Color.Red;
                    }
                }
            }
            button1.Enabled = true;
            panel1.Visible = false;
        }
        public void check_data_change(CheckBox c, int bk,string state)
        {
            if (state=="True")
            {
                if (c.Name.Contains("aa") || c.Name.Contains("bb") || c.Name.Contains("cc"))
                    ar.Text = Convert.ToString(Convert.ToInt32(ar.Text) + (bk == 0 ?1750:7500));
                else if (c.Name.StartsWith("a") || c.Name.StartsWith("b") || c.Name.StartsWith("c"))
                    ar.Text = Convert.ToString(Convert.ToInt32(ar.Text) + (bk == 0 ?2500 : 10000 ));
                osszeg.Text = Convert.ToString(Convert.ToInt32(osszeg.Text)+1);
            }
            else 
            {
                if (c.Name.Contains("aa") || c.Name.Contains("bb") || c.Name.Contains("cc"))
                    ar.Text = Convert.ToString(Convert.ToInt32(ar.Text) - (bk == 0 ? 1750 : 7500));
                else if (c.Name.StartsWith("a") || c.Name.StartsWith("b") || c.Name.StartsWith("c"))
                    ar.Text = Convert.ToString(Convert.ToInt32(ar.Text) - (bk == 0 ? 2500 : 10000));
                osszeg.Text = Convert.ToString(Convert.ToInt32(osszeg.Text) - 1);
            }
            eredeti_ar = Convert.ToInt32(ar.Text);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string valasztott = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (seats[i, j].Checked)
                    {
                        ulesek[i, j] = 2;
                        valasztott+=seats[i, j].Name+",";
                        if (j<8) jegyszam1++;
                        else jegyszam2++;
                    }
                }
            }
            koronalizalo();
            
            db.openconnection();
            cmd = new SQLiteCommand("insert into vasarlas values('" + vnev.Text + "','" +utvonal.Text +"','" + jegyszam2 + "','" +jegyszam1+"','" + ar.Text + "','"+kedvezmeny+ "','"
                + valasztott.Substring(0,valasztott.Length-1) + "','" + kupon.Text + "','"+tol.SelectedItem.ToString() + "','" + ig.SelectedItem.ToString() + "')", db.GetConnection());
            cmd.ExecuteNonQuery();
            db.closeconnection();
            MessageBox.Show("Sikeres vásárlás! \nSuccessful purchase!");
            this.Hide();
            User u = new User();
            u.Show();
        }
        private void onCheckBoxChecked(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            check_data_change(c, mode, c.Checked.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Készítette: Holicza Barnabás");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tol.Items.Clear();
            tol.Text = "";
            ig.Items.Clear();
            ig.Text = "";
            nem.Text = "Belföldi";
            mode = 0;
            string[] megallok = { "Veszprém", "Székesfehérvár", "Budapest",
                "Szeged", "Hódmezővásárhely", "Szentes", "Kecskemét"};
            for (int i = 0; i < megallok.Length; i++)
            {
                tol.Items.Add(megallok[i]);
            }
            ig.Enabled = false;
            Tól.Text = "Honnan";
            label26.Text = "Hova";
            label25.Text = "Vásárló neve:";
            label28.Text = "Jegyszám:";
            label27.Text = "Kupon:";
            label30.Text = "Végösszeg:";
            button1.Text = "Vásárlás";
            button1.Enabled = false;
            panel1.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tol.Items.Clear();
            tol.Text = "";
            ig.Items.Clear();
            ig.Text = "";
            nem.Text = "International";
            mode = 1;
            string[] megallok = { "Budapest", "Pozsony", "Prága", "Drezda", "Berlin", "Hamburg"
            , "Bécs", "Klagenfurt", "Velence", "Róma"};
            for (int i = 0; i < megallok.Length; i++)
            {
                tol.Items.Add(megallok[i]);
            }
            ig.Enabled = false;
            Tól.Text = "From";
            label26.Text = "To";
            label25.Text = "Name:";
            label28.Text = "Number of tickets:";
            label27.Text = "Coupon:";
            label30.Text = "Total:";
            button1.Text = "Buy";
            button1.Enabled = false;
            panel1.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 lo = new Form1();
            lo.Show();
        }

        private void kupon_TextChanged(object sender, EventArgs e)
        {

            if (kupon.Text == "K5")
            {
                ar.Text = Convert.ToString(Convert.ToInt32(ar.Text) * 0.95);
                kedvezmeny= Convert.ToDouble(ar.Text) * 0.05;
            }
            else if (kupon.Text == "K10")
            {
                ar.Text = Convert.ToString(Convert.ToInt32(ar.Text) * 0.90);
                kedvezmeny = Convert.ToDouble(ar.Text) * 0.1;
            }
            else
            {
                ar.Text = Convert.ToString(eredeti_ar);
                kedvezmeny=0;
            }
        }
    }
}
