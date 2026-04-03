using CSMS.Controllers.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSMS.Views
{
    public partial class UserControlDashbord : UserControl
    {
        MainController mainController = new MainController();
        public UserControlDashbord()
        {
            InitializeComponent();
        }

        public void Count()
        {
            lblTotalProduct.Text = mainController.DashBord("SELECT COUNT(*) FROM Product;");
            lblTotalOrders.Text = mainController.DashBord("SELECT COUNT(*) FROM Orders WHERE Payment_Status = 'Not Paid';");
            lblLowStock.Text = mainController.DashBord("SELECT COUNT(*) FROM Product WHERE Product_Status = 'Not Available';");
            lblTotalRevenue.Text = mainController.DashBord("SELECT SUM(Grand_Total) FROM Orders;");
        }
        private void UserControlDashbord_Load(object sender, EventArgs e)
        {
            Count();
        }
    }
}
