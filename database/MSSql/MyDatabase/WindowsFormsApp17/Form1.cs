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

namespace WindowsFormsApp17
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConn = null;
        private string connstr = "SERVER="+ConfigurationManager.AppSettings["IP"]+","+ConfigurationManager.AppSettings["PORT"]+";" +
            "DATABASE="+ConfigurationManager.AppSettings["DBNAME"] +";UID="+ConfigurationManager.AppSettings["USERID"] +";PASSWORD="+ConfigurationManager.AppSettings["USERPASSWORD"];


        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 데이터베이스연결
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConn = new SqlConnection(connstr);
                //데이터베이스
                sqlConn.Open();
                MessageBox.Show("데이터베이스연결성공");
                Console.WriteLine("[알림]데이터베이스연결성공");
            }
            catch(Exception ex)
            {
                //알림창으로 에러내용
                MessageBox.Show("에러발생내용" + ex.ToString());
                Console.WriteLine("[오류]오류내용"+ex.ToString());
            }
        }

        /// <summary>
        /// 데이터베이스연결해제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn2_Click(object sender, EventArgs e)
        {
            try
            {
                //데이터베이스와 연결을 끊기
                //null이아닌 것은  sqlConn = new SqlConnection(connstr);이 실행됬음.
                if (sqlConn != null)
                {
                    sqlConn.Close();
                    MessageBox.Show("데이터베이스연결을 끊음");
                    Console.WriteLine("[알림]데이터베이스연결끊김");
                }
            }
            catch (Exception ex)
            {
                //알림창으로 에러내용
                MessageBox.Show("에러발생내용" + ex.ToString());
                Console.WriteLine("[오류]오류내용" + ex.ToString());
            }

        }
    }
}
