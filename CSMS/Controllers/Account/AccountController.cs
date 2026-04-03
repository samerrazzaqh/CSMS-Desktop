using CSMS.Data;
using CSMS.Models.Account;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CSMS.Controllers.Account
{
    public class AccountController
    {
        //MySqlConnection con;
        //MySqlCommand cmd;
        //MySqlDataReader dataReader;


        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dataReader;


        public void executeCommand(string query)
        {
            //cmd = new MySqlCommand(query, con);
            cmd = new SqlCommand(query, con);
        }

        //-----------------------------------------------------------------------------
        public DataTable DisplyAndSearch(string query)
        {
            con = DbConnection.GetConnection();
            executeCommand(query);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            con.Close();
            return dt;
        }
        public void ControllerUpdateUser(string UserName, string Email, string id)
        {
            string query = "UPDATE users SET Users_Name = @UserName, Users_Email = @Email  where Users_Id = @UsersId";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@UsersId", int.Parse(id));
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.ExecuteNonQuery();
            try
            {
                MessageBox.Show("Updated Successfully. ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Brand not update. \n" + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
        }
        public void ControllerDeleteUser(string id)
        {

            string query = "DELETE FROM users where Users_Id = @UsersId";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@UsersId", int.Parse(id));
            cmd.ExecuteNonQuery();

            try
            {
                MessageBox.Show("Deleted Successfully. ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("User not Delete. \n" + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();

        }
        //----------------------------------------------------------------------------------------
        public Login ControllerLogin(string Email)
        {
            Login login = new Login();
            string query = "select * from users where Users_Email=@Email";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@Email",Email);
            cmd.ExecuteNonQuery();
            dataReader = cmd.ExecuteReader();
            if (dataReader.Read() == true)
            {
                login.Email = dataReader["Users_Email"].ToString();
                login.Password = dataReader["Users_Password"].ToString();
                con.Close();
            }
            else
            {
                login = null;
                con.Close();
            }
            return login;
        }



        public void ControllerRegister(Register register)
        {
            //string query = "INSERT INTO users VALUES (NULL,@UserName,@Email,@Password)";
            string query = "INSERT INTO users VALUES (@UserName,@Email,@Password)";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@UserName", register.UserName);
            cmd.Parameters.AddWithValue("@Email", register.Email);
            cmd.Parameters.AddWithValue("@Password", register.Password);
            cmd.ExecuteNonQuery();

            try
            {
                MessageBox.Show("Add Successfully. ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Acount not insert. \n" + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
        }



        public Login ControllerGetEmail(string Email)
        {
            Login login = new Login();
            string query = "select * from users where Users_Email=@Email";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.ExecuteNonQuery();
            dataReader = cmd.ExecuteReader();
            if (dataReader.Read() == true)
            {
                login.Email = dataReader["Users_Email"].ToString();
                con.Close();
            }
            else
            {
                login = null;
                con.Close();
            }
            return login;
        }

        public void ControllerUpdatePassword(string Email,string Password)
        {
            string query = "UPDATE users SET Users_Password = @Password  where Users_Email = @Email";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.ExecuteNonQuery();
            try
            {
                MessageBox.Show("Updated Successfully. ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Password not update. \n" + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
        }

















    }
}
