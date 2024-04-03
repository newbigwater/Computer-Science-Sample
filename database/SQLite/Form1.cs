using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string strConnection = @"Data Source=./haksa";
            SQLiteConnection conn = new SQLiteConnection(strConnection);
            conn.Open();

            string SQL = "SELECT * FROM student";
            SQLiteCommand cmd = new SQLiteCommand(SQL, conn);
            SQLiteDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                listBox1.Items.Add(dr["id"].ToString() + "\t" +
                                   dr["name"].ToString() + "\t" +
                                   dr["age"].ToString() + "\t" +
                                   dr["phone"].ToString());
            }
            dr.Close();
            conn.Close();
        }
    }
}
