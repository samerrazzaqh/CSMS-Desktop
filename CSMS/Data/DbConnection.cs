using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSMS.Data
{
    class DbConnection
    {
        //-----------------------------------SqlConnection----------------------------------------
        public static SqlConnection GetConnection()
        {
             string sql = "Data Source=SAMERRAZZAQA8E0\\SQLEXPRESS;Initial Catalog=csms;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //string sql = "Server=SAMERRAZZAQBE8F\\localhost;Database=csms;TrustServerCertificate=True;User=sa;Password=root;";
            //string sql = "server=localhost;user=root;passwoed=;database=csms";
            SqlConnection con = new SqlConnection(sql);
            try
            {
                con.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Sql Connection !! \n" + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return con;
        }


        //-----------------------------------MySqlConnection----------------------------------------
        //public static MySqlConnection GetConnection()
        //{
        //    string sql = "server=localhost;user=root;passwoed=;database=csms";
        //    MySqlConnection con = new MySqlConnection(sql);
        //    try
        //    {
        //        con.Open();
        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show("MySql Connection !! \n" + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //    }
        //    return con;
        //}











    }
}
