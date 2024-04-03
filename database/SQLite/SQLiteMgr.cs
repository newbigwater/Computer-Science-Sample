using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLite
{


    /*
     * [NuGet Package 추가하기]


     * */
    /// <summary>
    /// 도구 -> NuGet 패키지 관리자 -> 패키지 관리자 콘솔
    /// <br/>Install-Package System.Data.SQLite 입력하여 설치
    /// </summary>
    public class SQLiteMgr
    {
        private static string dataSource = @"Data Source=./haksa";
        SQLiteDataAdapter adpt;

        public DataSet SelectAll(string table)
        {
            try
            {
                DataSet ds = new DataSet();

                string sql = $"SELECT * FROM {table}";
                adpt = new SQLiteDataAdapter(sql, dataSource);
                adpt.Fill(ds, table);

                if (ds.Tables.Count > 0) return ds;
                else return null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }

        public DataSet SelectDetail(string table, string condition, string where = "")
        {
            try
            {
                DataSet ds = new DataSet();

                string sql = $"SELECT {condition} FROM {table} {where}";
                adpt = new SQLiteDataAdapter(sql, dataSource);
                adpt.Fill(ds, table);

                if (ds.Tables.Count > 0) return ds;
                else return null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }

        public void Insert(string table, string value)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(dataSource))
                {
                    conn.Open();
                    string sql = $"INSERT INTO {table} VALUES ({value})";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }

        public void Update(string table, string setvalue, string wherevalue = "")
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(dataSource))
                {
                    conn.Open();
                    string sql = $"UPDATE {table} SET {setvalue} WHERE {wherevalue}";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }

        public void DeleteAll(string table)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(dataSource))
                {
                    conn.Open();
                    string sql = $"DELETE FROM {table}";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }

        public void DeleteDetail(string table, string wherecol, string wherevalue)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(dataSource))
                {
                    conn.Open();
                    string sql = $"DELETE FROM {table} WHERE {wherecol}='{wherevalue}'";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }
    }
}
