using CSMS.Data;
using CSMS.Models.Account;
using CSMS.Models.Main;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace CSMS.Controllers.Main
{
    public class MainController
    {
        //MySqlConnection con;
        //MySqlCommand cmd;
        //MySqlDataReader dataReader;
        //MySqlDataAdapter sqlDataAdapter;


        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dataReader;
        SqlDataAdapter sqlDataAdapter;



        public void executeCommand(string query)
        {
            //cmd = new MySqlCommand(query, con);
            cmd = new SqlCommand(query, con);
        }
        public void executesqlDataAdapter(string query)
        {
            //sqlDataAdapter = new MySqlDataAdapter(query, con);
            sqlDataAdapter = new SqlDataAdapter(query, con);

        }

        public DataTable DisplyAndSearch(string query)
        {
            con = DbConnection.GetConnection();
            executeCommand(query);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            con.Close();
            return dt;
        }
        //--------------------------Brand---------------------------------------------------------
        public void ControllerAddBrand(MainBrand mainBrand)
        {
            //string query = "INSERT INTO Brand VALUES (NULL,@BrandName,@BrandStatus)";
            string query = "INSERT INTO Brand VALUES (@BrandName,@BrandStatus)";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@BrandName", mainBrand.BrandName);
            cmd.Parameters.AddWithValue("@BrandStatus", mainBrand.BrandStatus);
            cmd.ExecuteNonQuery();

            try
            {
                MessageBox.Show("Add Successfully. ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Brand not insert. \n" + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
        }
        public void ControllerUpdateBrand(string BrandName, string BrandStatus, string id)
        {
            string query = "UPDATE Brand SET Brand_Name = @BrandName, Brand_Status = @BrandStatus  where Brand_Id = @BrandId";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@BrandId", int.Parse(id));
            cmd.Parameters.AddWithValue("@BrandName", BrandName);
            cmd.Parameters.AddWithValue("@BrandStatus", BrandStatus);
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
        public void ControllerDeleteBrand(string id)
        {

            string query = "DELETE FROM Brand where Brand_Id = @BrandId";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@BrandId", int.Parse(id));
            cmd.ExecuteNonQuery();

            try
            {
                MessageBox.Show("Deleted Successfully. ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Brand not Delete. \n" + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();

        }

        //--------------------------Category---------------------------------------------------------
        public void ControllerAddCategory(MainCategory mainCategory)
        {
            //string query = "INSERT INTO Category VALUES (NULL,@CategoryName,@CategoryStatus)";
            string query = "INSERT INTO Category VALUES (@CategoryName,@CategoryStatus)";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@CategoryName", mainCategory.CategoryName);
            cmd.Parameters.AddWithValue("@CategoryStatus", mainCategory.CategoryStatus);
            cmd.ExecuteNonQuery();

            try
            {
                MessageBox.Show("Add Successfully. ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Category not insert. \n" + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
        }
        public void ControllerUpdateCategory(string CategoryName, string CategoryStatus, string id)
        {
            string query = "UPDATE Category SET Category_Name = @CategoryName, Category_Status = @CategoryStatus  where Category_Id = @CategoryId";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@CategoryId", int.Parse(id));
            cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
            cmd.Parameters.AddWithValue("@CategoryStatus", CategoryStatus);
            cmd.ExecuteNonQuery();
            try
            {
                MessageBox.Show("Updated Successfully. ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Category not update. \n" + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
        }
        public void ControllerDeleteCategory(string id)
        {

            string query = "DELETE FROM Category where Category_Id = @CategoryId";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@CategoryId", int.Parse(id));
            cmd.ExecuteNonQuery();

            try
            {
                MessageBox.Show("Deleted Successfully. ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Category not Delete. \n" + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();

        }

        //--------------------------DashBord--------------------------------------------------------
        public string DashBord(string query)
        {
            int getfield =0;
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.ExecuteNonQuery();
            dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                if (dataReader[0] != DBNull.Value)
                {
                    getfield = Convert.ToInt32(dataReader[0]);
                }
                else
                {
                    getfield = 0;
                }
               
            }
            con.Close();
            return getfield.ToString();
        }
        //--------------------------Product---------------------------------------------------------

        public DataTable BrandCategoryProduct(string query)
        {
            con = DbConnection.GetConnection();
            executeCommand(query);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            //DataRow row = dt.NewRow();
            //row[0] = "-- SELECT --";
            //dt.Rows.InsertAt(row, 0);
            con.Close();
            return dt;
        }
        public void ControllerAddProduct(MainProduct mainProduct)
        {
            //string query = "INSERT INTO Category VALUES (NULL,@CategoryName,@CategoryStatus)";
            string query = "INSERT INTO Product VALUES (@ProductName,@ProductImage,@ProductRate,@ProductQuantity,@ProductBrand,@ProductCategory,@ProductStatus)";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@ProductName", mainProduct.ProductName);
            cmd.Parameters.AddWithValue("@ProductImage", mainProduct.ProductImage);
            cmd.Parameters.AddWithValue("@ProductRate", mainProduct.ProductRate);
            cmd.Parameters.AddWithValue("@ProductQuantity", mainProduct.ProductQuantity);
            cmd.Parameters.AddWithValue("@ProductBrand", mainProduct.ProductBrand);
            cmd.Parameters.AddWithValue("@ProductCategory", mainProduct.ProductCategory);
            cmd.Parameters.AddWithValue("@ProductStatus", mainProduct.ProductStatus);
            cmd.ExecuteNonQuery();

            try
            {
                MessageBox.Show("Add Successfully. ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Product not insert. \n" + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
        }
        public void ControllerUpdateProduct(string ProductName, byte[] ProductImage, int ProductRate, int ProductQuantity, string ProductBrand, string ProductCategory, string ProductStatus, string id)
        {
            string query = "UPDATE Product SET Product_Name = @ProductName, Product_Image = @ProductImage , Product_Rate = @ProductRate, Product_Quantity = @ProductQuantity , Product_Brand = @ProductBrand , Product_Category = @ProductCategory, Product_Status = @ProductStatus where Product_Id = @ProductId";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@ProductId", int.Parse(id));
            cmd.Parameters.AddWithValue("@ProductName", ProductName);
            cmd.Parameters.AddWithValue("@ProductImage", ProductImage);
            cmd.Parameters.AddWithValue("@ProductRate", ProductRate);
            cmd.Parameters.AddWithValue("@ProductQuantity", ProductQuantity);
            cmd.Parameters.AddWithValue("@ProductBrand", ProductBrand);
            cmd.Parameters.AddWithValue("@ProductCategory", ProductCategory);
            cmd.Parameters.AddWithValue("@ProductStatus", ProductStatus);
            cmd.ExecuteNonQuery();
            try
            {
                MessageBox.Show("Updated Successfully. ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Product not update. \n" + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
        }
        public void ControllerDeleteProduct(string id)
        {

            string query = "DELETE FROM Product where Product_Id = @ProductId";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@ProductId", int.Parse(id));
            cmd.ExecuteNonQuery();

            try
            {
                MessageBox.Show("Deleted Successfully. ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Product not Delete. \n" + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();

        }

        //--------------------------Order---------------------------------------------------------

        public DataTable Product(string query)
        {
            con = DbConnection.GetConnection();
            executeCommand(query);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            //DataRow row = dt.NewRow();
            //row[0] = "-- SELECT --";
            //dt.Rows.InsertAt(row, 0);
            con.Close();
            return dt;
        }
        public string GetProductRate(string query, string GetField)
        {
            string getfield;
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.ExecuteNonQuery();
            dataReader = cmd.ExecuteReader();
            if (dataReader.Read() == true)
            {
                getfield = dataReader[GetField].ToString();
                con.Close();
            }
            else
            {
                getfield = null;
                con.Close();
            }
            return getfield;
        }

        public void ControllerAddOrder(MainOrder mainOrder)
        {
            string query = "INSERT INTO Orders VALUES (@OrdersDate,@CustomerName,@CustomerNumber,@TotalAmount,@PaidAmount,@DueAmount,@Discount,@GrandTotal,@PaymentStatus)";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@OrdersDate", mainOrder.OrdersDate);
            cmd.Parameters.AddWithValue("@CustomerName", mainOrder.CustomerName);
            cmd.Parameters.AddWithValue("@CustomerNumber", mainOrder.CustomerNumber);
            cmd.Parameters.AddWithValue("@TotalAmount", mainOrder.TotalAmount);
            cmd.Parameters.AddWithValue("@PaidAmount", mainOrder.PaidAmount);
            cmd.Parameters.AddWithValue("@DueAmount", mainOrder.DueAmount);
            cmd.Parameters.AddWithValue("@Discount", mainOrder.Discount);
            cmd.Parameters.AddWithValue("@GrandTotal", mainOrder.GrandTotal);
            cmd.Parameters.AddWithValue("@PaymentStatus", mainOrder.PaymentStatus);
            cmd.ExecuteNonQuery();

            try
            {
                MessageBox.Show("Add Successfully. ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Product not insert. \n" + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
        }
        public void ControllerUpdateOrder(DateTime OrdersDate, string CustomerName, string CustomerNumber, int TotalAmount, int PaidAmount, int DueAmount,int Discount,int GrandTotal, string PaymentStatus, string id)
        {
            string query = "UPDATE Orders SET Orders_Date = @OrdersDate, Customer_Name = @CustomerName, Customer_Number = @CustomerNumber, Total_Amount = @TotalAmount, Paid_Amount = @PaidAmount, Due_Amount = @DueAmount, Discount = @Discount, Grand_Total = @GrandTotal, Payment_Status = @PaymentStatus where Orders_Id = @OrdersId";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@OrdersId", int.Parse(id));
            cmd.Parameters.AddWithValue("@OrdersDate", OrdersDate);
            cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
            cmd.Parameters.AddWithValue("@CustomerNumber", CustomerNumber);
            cmd.Parameters.AddWithValue("@TotalAmount", TotalAmount);
            cmd.Parameters.AddWithValue("@PaidAmount", PaidAmount);
            cmd.Parameters.AddWithValue("@DueAmount", DueAmount);
            cmd.Parameters.AddWithValue("@Discount", Discount);
            cmd.Parameters.AddWithValue("@GrandTotal", GrandTotal);
            cmd.Parameters.AddWithValue("@PaymentStatus", PaymentStatus);
            cmd.ExecuteNonQuery();
            try
            {
                MessageBox.Show("Updated Successfully. ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Order not update. \n" + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
        }
        public void ControllerDeleteOrder(string id)
        {

            string query = "DELETE FROM Orders where Orders_Id = @OrdersId";
            con = DbConnection.GetConnection();
            executeCommand(query);
            cmd.Parameters.AddWithValue("@OrdersId", int.Parse(id));
            cmd.ExecuteNonQuery();

            try
            {
                MessageBox.Show("Deleted Successfully. ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Order not Delete. \n" + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();

        }


        //--------------------------Report---------------------------------------------------------
        public DataTable Report(string date)
        {
            con = DbConnection.GetConnection();
            executesqlDataAdapter("SELECT * FROM Orders WHERE Orders_Date LIKE '" + date + "'");
            DataTable tb1 = new DataTable();
            sqlDataAdapter.Fill(tb1);
            con.Close();
            return tb1;
        }


        //-----------------------------------------------------------------------------------------

    }
}
