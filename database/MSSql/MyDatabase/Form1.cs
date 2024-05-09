using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDatabase
{
    public partial class Form1 : Form
    {
        private SqlConnection _sqlConn = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_database_isExist_Click(object sender, EventArgs e)
        {
            SqlConnection sqlMasterConn = null;
            string connstr = "SERVER=" + ConfigurationManager.AppSettings["IP"] + "," + ConfigurationManager.AppSettings["PORT"] + ";" +
                             "DATABASE=" + ConfigurationManager.AppSettings["DBNAME"] + ";UID=" + ConfigurationManager.AppSettings["USERID"] + ";PASSWORD=" + ConfigurationManager.AppSettings["USERPASSWORD"];
        }

        private void btn_database_create_Click(object sender, EventArgs e)
        {

        }
    }
}
