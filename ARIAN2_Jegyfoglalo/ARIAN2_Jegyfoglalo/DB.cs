﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ARIAN2_Jegyfoglalo
{
    class DB
    {
        private SQLiteConnection con = new SQLiteConnection("data source=adatbazis.db");

        public SQLiteConnection GetConnection()
        {
            return con;
        }

        public void openconnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
        }

        public void closeconnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
        }
    }
}
