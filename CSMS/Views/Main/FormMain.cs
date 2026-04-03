using CSMS.Views.Account;
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
    public partial class FormMain : Form
    {
        public string username;
        public FormMain()
        {
            InitializeComponent();
            this.Size = new Size(1050, 650);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            lblUsername.Text = username;
            lblTimeAndDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you want to log out ?", "Question",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Close();
            }
        }

        private void btnDashbord_Click(object sender, EventArgs e)
        {
            userControlDashbord1.Visible = true;
            userControlDashbord1.Count();
            userControlBrand1.Visible = false;
            userControlCategory1.Visible = false;
            userControlProduct1.Visible = false;
            userControlOrder1.Visible = false;
            userControlReport1.Visible = false;
            userControlUser1.Visible = false;
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            userControlDashbord1.Visible = false;
            userControlBrand1.Visible = true;
            userControlBrand1.EmptyBox();
            userControlCategory1.Visible = false;
            userControlProduct1.Visible = false;
            userControlOrder1.Visible = false;
            userControlReport1.Visible = false;
            userControlUser1.Visible = false;
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            userControlDashbord1.Visible = false;
            userControlBrand1.Visible = false;
            userControlCategory1.Visible = true;
            userControlCategory1.EmptyBox();
            userControlProduct1.Visible = false;
            userControlOrder1.Visible = false;
            userControlReport1.Visible = false;
            userControlUser1.Visible = false;

        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            userControlProduct1.Visible = true ;
            userControlDashbord1.Visible = false;
            userControlBrand1.Visible = false;
            userControlProduct1.EmptyBox();
            userControlCategory1.Visible = false;
            userControlOrder1.Visible = false;
            userControlReport1.Visible = false;
            userControlUser1.Visible = false;
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            userControlDashbord1.Visible = false;
            userControlBrand1.Visible = false;
            userControlCategory1.Visible = false;
            userControlProduct1.Visible = false;
            userControlOrder1.Visible = true;
            userControlOrder1.EmptyBox();
            userControlReport1.Visible = false;
            userControlUser1.Visible = false;
        }
        private void Report_Click(object sender, EventArgs e)
        {
            userControlDashbord1.Visible = false;
            userControlBrand1.Visible = false;
            userControlCategory1.Visible = false;
            userControlProduct1.Visible = false;
            userControlOrder1.Visible = false;
            userControlReport1.Visible = true;
            userControlUser1.Visible = false;
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            userControlDashbord1.Visible = false;
            userControlBrand1.Visible = false;
            userControlCategory1.Visible = false;
            userControlProduct1.Visible = false;
            userControlOrder1.Visible = false;
            userControlReport1.Visible = false;
            userControlUser1.Visible = true;
            userControlUser1.EmptyBox();
        }
    }
}
