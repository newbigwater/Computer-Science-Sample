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

        public bool IsExistDatabase(string databaseName)
        {
            string masterConnInfo = $"" +
                                    $"Integrated Security=SSPI;" +
                                    $"connection timeout=30;" +
                                    $"server={ConfigurationManager.AppSettings["IP"]}, {ConfigurationManager.AppSettings["PORT"]};";
            bool isExisted = false;
            SqlConnection masterConn = new SqlConnection(masterConnInfo);
            masterConn.Open();

            //Database
            {
                //                 string query = string.Format($"select * from sys.Sysdatabases " + $"where name='{edt_database.Text}';");
                //                 SqlCommand masterCmd = new SqlCommand(query, masterConn);
                //                 {
                //                     SqlDataReader reader = masterCmd.ExecuteReader();
                //                     {
                //                         isExisted = reader.HasRows;
                //                     }
                //                     reader.Close();
                //                     reader.Dispose();
                //                 }

                //                 if (isExisted)
                //                     MessageBox.Show($"{edt_database.Text} Database가 존재합니다.", "Is Exist?", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                 else
                //                     MessageBox.Show($"{edt_database.Text} Database가 존재하지 않습니다.", "Is Exist?", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                string query = $"IF EXISTS (SELECT * FROM sys.databases WHERE name = '{databaseName}') SELECT 1 ELSE SELECT 0";
                using (SqlCommand command = new SqlCommand(query, masterConn))
                {
                    int result = (int)command.ExecuteScalar();

                    if (result == 1)
                        return true;
                    else
                        return false;
                }
            }
        }

        private void btn_database_isExist_Click(object sender, EventArgs e)
        {
            if(IsExistDatabase(edt_database.Text))
                MessageBox.Show($"{edt_database.Text} Database가 존재합니다.", "Is Exist?", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"{edt_database.Text} Database가 존재하지 않습니다.", "Is Exist?", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void btn_database_create_Click(object sender, EventArgs e)
        {
            string databaseName = $"{edt_database.Text}";  // 확인하고자 하는 데이터베이스 이름

            if (IsExistDatabase(databaseName))
            {
                MessageBox.Show($"{databaseName} Database가 이미 존재합니다.", "Is Exist?", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string masterConnInfo = $"" +
                                    $"Integrated Security=SSPI;" +
                                    $"connection timeout=30;" +
                                    $"server={ConfigurationManager.AppSettings["IP"]}, {ConfigurationManager.AppSettings["PORT"]};";
            using (SqlConnection connection = new SqlConnection(masterConnInfo))
            {
                connection.Open();

                string query = $"CREATE DATABASE [{databaseName}];";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            if (IsExistDatabase(databaseName))
                MessageBox.Show($"{databaseName} Database가 생성되었습니다.", "Create", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"{databaseName} Database가 생성되지 못했습니다.", "Create", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btn_database_drop_Click(object sender, EventArgs e)
        {
            string databaseName = $"{edt_database.Text}";  // 확인하고자 하는 데이터베이스 이름

            if (!IsExistDatabase(databaseName))
            {
                MessageBox.Show($"{databaseName} Database가 존재하지 않습니다.", "Is Exist?", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string masterConnInfo = $"" +
                                    $"Integrated Security=SSPI;" +
                                    $"connection timeout=30;" +
                                    $"server={ConfigurationManager.AppSettings["IP"]}, {ConfigurationManager.AppSettings["PORT"]};";
            using (SqlConnection connection = new SqlConnection(masterConnInfo))
            {
                connection.Open();
                
                string query = $@"
                IF DB_ID(N'{databaseName}') IS NOT NULL
                BEGIN
                    ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    DROP DATABASE [{databaseName}];
                END";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            if (IsExistDatabase(databaseName))
                MessageBox.Show($"{databaseName} Database가 삭제되지 않았습니다.", "Drop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show($"{databaseName} Database가 삭제되었습니다.", "Drop", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool IsExistTable(string databaseName, string tableName)
        {
            string connInfo = $"" +
                                    $"Integrated Security=False;" +
                                    $"connection timeout=30;" +
                                    $"server={ConfigurationManager.AppSettings["IP"]}, {ConfigurationManager.AppSettings["PORT"]};" +
                                    $"database={databaseName};" +
                                    $"user id={ConfigurationManager.AppSettings["USERID"]};" +
                                    $"password={ConfigurationManager.AppSettings["USERPASSWORD"]};";
            SqlConnection masterConn = new SqlConnection(connInfo);
            masterConn.Open();

            //Database
            {
                string query = $"IF EXISTS (SELECT * FROM sys.databases WHERE name = '{databaseName}') SELECT 1 ELSE SELECT 0";
                using (SqlCommand command = new SqlCommand(query, masterConn))
                {
                    int result = (int)command.ExecuteScalar();

                    if (result == 0)
                    {
                        MessageBox.Show($"{databaseName} Database가 존재하지 않습니다.", "Is Exist?", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }

            //Table
            {
                string query = $"IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '{tableName}') " +
                               $"SELECT 1 ELSE SELECT 0";
                using (SqlCommand command = new SqlCommand(query, masterConn))
                {
                    int result = (int)command.ExecuteScalar();

                    if (result == 1)
                        return true;
                    else
                        return false;
                }
            }
        }

        private void btn_table_isExist_Click(object sender, EventArgs e)
        {
            if(IsExistTable(edt_database.Text, edt_table.Text))
                MessageBox.Show($"{edt_table.Text} Table이 존재합니다.", "Is Exist?", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"{edt_table.Text} Table이 존재하지 않습니다.", "Is Exist?", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btn_table_create_Click(object sender, EventArgs e)
        {
            string databaseName = $"{edt_database.Text}";
            string tableName = $"{edt_table.Text}";

            if (IsExistTable(databaseName, tableName))
            {
                MessageBox.Show($"{tableName} Table이 이미 존재합니다.", "Is Exist?", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connInfo = $"" +
                              $"Integrated Security=False;" +
                              $"connection timeout=30;" +
                              $"server={ConfigurationManager.AppSettings["IP"]}, {ConfigurationManager.AppSettings["PORT"]};" +
                              $"database={databaseName};" +
                              $"user id={ConfigurationManager.AppSettings["USERID"]};" +
                              $"password={ConfigurationManager.AppSettings["USERPASSWORD"]};";
            using (SqlConnection connection = new SqlConnection(connInfo))
            {
                connection.Open();
                
                string query = $"CREATE TABLE {tableName} (ID INT PRIMARY KEY IDENTITY(1,1), Name NVARCHAR(100), Age INT);";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            if (IsExistTable(databaseName, tableName))
                MessageBox.Show($"{tableName} Table이 생성되었습니다.", "Create", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"{tableName} Table이 생성되지 못했습니다.", "Create", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btn_table_drop_Click(object sender, EventArgs e)
        {
            string databaseName = $"{edt_database.Text}";
            string tableName = $"{edt_table.Text}";

            if (!IsExistTable(databaseName, tableName))
            {
                MessageBox.Show($"{tableName} Table이 존재하지 않습니다.", "Is Exist?", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connInfo = $"" +
                              $"Integrated Security=False;" +
                              $"connection timeout=30;" +
                              $"server={ConfigurationManager.AppSettings["IP"]}, {ConfigurationManager.AppSettings["PORT"]};" +
                              $"database={databaseName};" +
                              $"user id={ConfigurationManager.AppSettings["USERID"]};" +
                              $"password={ConfigurationManager.AppSettings["USERPASSWORD"]};";
            using (SqlConnection connection = new SqlConnection(connInfo))
            {
                connection.Open();

                string query = $"DROP TABLE dbo.{tableName};";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            if (IsExistTable(databaseName, tableName))
                MessageBox.Show($"{tableName} Table이 삭제되지 않았습니다.", "Create", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show($"{tableName} Table이 삭제되었습니다.", "Create", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
