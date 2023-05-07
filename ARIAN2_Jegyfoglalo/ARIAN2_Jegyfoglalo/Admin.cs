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
    public partial class Admin : Form
    {
        DB db = new DB();
        SQLiteDataAdapter sda;
        DataTable dt;
        SQLiteCommand cmd;
        int mode;
        int[,] ulesek = new int[3, 16];
        CheckBox[,] seats;
        private void Admin_Load(object sender, EventArgs e)
        {
            nem.Text = "";
            utvonal.Text = "";
            MessageBox.Show("Kérem válasszon nyelvet a menetrendek megtekintéséhez!\n" +
                "Please choose a language to see the available railway guides!");
        }
        public void feltolto()
        {
            db.openconnection();
            cmd = new SQLiteCommand("select * from ulesek where Menetrend='" + utvonal.Text + "'", db.GetConnection());
            sda = new SQLiteDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            db.closeconnection();
            int a = 1;
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
            db.openconnection();
            cmd = new SQLiteCommand("select * from vasarlas where Menetrend='" + utvonal.Text + "'", db.GetConnection());
            sda = new SQLiteDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            db.closeconnection();
        }
        public Admin()
        {
            InitializeComponent();
            seats = new CheckBox[,]
            {{a1,a2,a3,a4,a5,a6,a7,a8, aa1, aa2, aa3, aa4, aa5, aa6, aa7, aa8 },
            { b1,b2,b3,b4,b5,b6,b7,b8, bb1, bb2, bb3, bb4, bb5, bb6, bb7, bb8 } ,
            { c1,c2,c3,c4,c5,c6,c7,c8, cc1, cc2, cc3, cc4, cc5, cc6, cc7, cc8 } };
        }

        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void teljesBevételToolStripMenuItem_Click(object sender, EventArgs e)
        {
            db.openconnection();
            chart1.Show();
            chart1.Series["Ar"].IsValueShownAsLabel = true;
            chart1.Series["Ar"].Color = Color.Green;
            foreach (var x in chart1.Series)
            {
                x.Points.Clear();
            }
            cmd = new SQLiteCommand("select * from vasarlas where Menetrend='" + utvonal.Text + "'", db.GetConnection());
            cmd.ExecuteNonQuery();
            dt = new DataTable();
            sda = new SQLiteDataAdapter(cmd);
            sda.Fill(dt);
            int arr = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                arr += Convert.ToInt32(dt.Rows[i][4]);
            }
            this.chart1.Series["Ar"].Points.AddXY("Teljes bevétel",arr);
            db.closeconnection();
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
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
            teljesBevételToolStripMenuItem.Text = "Teljes bevétel";
            teljesBevételJegytípusonkéntToolStripMenuItem.Text = "Tejles bevétel jegytípusonként";
        }

        private void hToolStripMenuItem_Click_1(object sender, EventArgs e)
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
            teljesBevételToolStripMenuItem.Text = "Total Income";
            teljesBevételJegytípusonkéntToolStripMenuItem.Text= "Total Income By Ticket Type";
        }

        private void tol_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (mode == 1)
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
        }

        private void ig_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    teljesBevételToolStripMenuItem.Enabled = true;
                    teljesBevételJegytípusonkéntToolStripMenuItem.Enabled = true;
                }
                else if (Bécs_Róma.Contains(tol.SelectedItem.ToString()) &&
                    Bécs_Róma.Contains(ig.SelectedItem.ToString()))
                {
                    utvonal.Text = "Bécs-Róma";
                    feltolto();
                    teljesBevételToolStripMenuItem.Enabled = true;
                    teljesBevételJegytípusonkéntToolStripMenuItem.Enabled = true;
                }
                else
                {
                    utvonal.Text = "Nincs ilyen útvonal!";
                    teljesBevételToolStripMenuItem.Enabled = false;
                    teljesBevételJegytípusonkéntToolStripMenuItem.Enabled = false;
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
                    teljesBevételToolStripMenuItem.Enabled = true;
                    teljesBevételJegytípusonkéntToolStripMenuItem.Enabled = true;
                }
                else if (Szeged_Kecskemét.Contains(tol.SelectedItem.ToString()) &&
                    Szeged_Kecskemét.Contains(ig.SelectedItem.ToString()))
                {
                    utvonal.Text = "Szeged-Kecskemét";
                    feltolto();
                    teljesBevételToolStripMenuItem.Enabled = true;
                    teljesBevételJegytípusonkéntToolStripMenuItem.Enabled = true;
                }
                else
                {
                    utvonal.Text = "Nincs ilyen útvonal!";
                    teljesBevételToolStripMenuItem.Enabled = false;
                    teljesBevételJegytípusonkéntToolStripMenuItem.Enabled = false;
                }

            }
        }

        private void infoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Készítette: Holicza Barnabás");
        }

        private void logOutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 lo = new Form1();
            lo.Show();
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void teljesBevételJegytípusonkéntToolStripMenuItem_Click(object sender, EventArgs e)
        {
            db.openconnection();
            chart1.Show();
            chart1.Series["Ar"].IsValueShownAsLabel = true;
            chart1.Series["Ar"].Color = Color.Green;
            foreach (var x in chart1.Series)
            {
                x.Points.Clear();
            }
            cmd = new SQLiteCommand("select * from vasarlas where Menetrend='" + utvonal.Text + "'", db.GetConnection());
            cmd.ExecuteNonQuery();
            dt = new DataTable();
            sda = new SQLiteDataAdapter(cmd);
            sda.Fill(dt);
            int db_szam1 = 0, db_szam2 = 0;
            double osszeg1 = 0, osszeg2 =0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                db_szam1 = Convert.ToInt32(dt.Rows[i][3]);
                db_szam2 = Convert.ToInt32(dt.Rows[i][2]);
                if ((db_szam1 * (mode == 0 ? 2500 : 10000) - Convert.ToInt32(dt.Rows[i][5])) > 0)
                    osszeg1 += (db_szam1 * (mode == 0 ? 2500 : 10000) -  (db_szam1 * Convert.ToInt32(dt.Rows[i][5])/(db_szam1+ db_szam2)));
                else osszeg1 += 0;

                if ((db_szam2 * (mode == 0 ? 1750 : 7500) - Convert.ToInt32(dt.Rows[i][5])) > 0)
                    osszeg2 += (db_szam2 * (mode == 0 ? 1750 : 7500) - (db_szam2 * Convert.ToInt32(dt.Rows[i][5]) / (db_szam1 + db_szam2)));
                else osszeg2 += 0;
            }

            this.chart1.Series["Ar"].Points.AddXY("1.osztály", osszeg1);
            this.chart1.Series["Ar"].Points.AddXY("2.osztály", osszeg2);
            db.closeconnection();
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
            teljesBevételToolStripMenuItem.Text = "Teljes bevétel";
            teljesBevételJegytípusonkéntToolStripMenuItem.Text = "Tejles bevétel jegytípusonként";
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
            teljesBevételToolStripMenuItem.Text = "Total Income";
            teljesBevételJegytípusonkéntToolStripMenuItem.Text = "Total Income By Ticket Type";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            db.openconnection();
            chart1.Show();
            chart1.Series["Ar"].IsValueShownAsLabel = true;
            chart1.Series["Ar"].Color = Color.Green;
            foreach (var x in chart1.Series)
            {
                x.Points.Clear();
            }
            cmd = new SQLiteCommand("select * from vasarlas where Menetrend='" + utvonal.Text + "'", db.GetConnection());
            cmd.ExecuteNonQuery();
            dt = new DataTable();
            sda = new SQLiteDataAdapter(cmd);
            sda.Fill(dt);
            int arr = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                arr += Convert.ToInt32(dt.Rows[i][4]);
            }
            this.chart1.Series["Ar"].Points.AddXY("Teljes bevétel", arr);
            db.closeconnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.openconnection();
            chart1.Show();
            chart1.Series["Ar"].IsValueShownAsLabel = true;
            chart1.Series["Ar"].Color = Color.Green;
            foreach (var x in chart1.Series)
            {
                x.Points.Clear();
            }
            cmd = new SQLiteCommand("select * from vasarlas where Menetrend='" + utvonal.Text + "'", db.GetConnection());
            cmd.ExecuteNonQuery();
            dt = new DataTable();
            sda = new SQLiteDataAdapter(cmd);
            sda.Fill(dt);
            int db_szam1 = 0, db_szam2 = 0;
            double osszeg1 = 0, osszeg2 = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                db_szam1 = Convert.ToInt32(dt.Rows[i][3]);
                db_szam2 = Convert.ToInt32(dt.Rows[i][2]);
                if ((db_szam1 * (mode == 0 ? 2500 : 10000) - Convert.ToInt32(dt.Rows[i][5])) > 0)
                    osszeg1 += (db_szam1 * (mode == 0 ? 2500 : 10000) - (db_szam1 * Convert.ToInt32(dt.Rows[i][5]) / (db_szam1 + db_szam2)));
                else osszeg1 += 0;

                if ((db_szam2 * (mode == 0 ? 1750 : 7500) - Convert.ToInt32(dt.Rows[i][5])) > 0)
                    osszeg2 += (db_szam2 * (mode == 0 ? 1750 : 7500) - (db_szam2 * Convert.ToInt32(dt.Rows[i][5]) / (db_szam1 + db_szam2)));
                else osszeg2 += 0;
            }

            this.chart1.Series["Ar"].Points.AddXY("1.osztály", osszeg1);
            this.chart1.Series["Ar"].Points.AddXY("2.osztály", osszeg2);
            db.closeconnection();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 lo = new Form1();
            lo.Show();
        }
    }
}
