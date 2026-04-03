using CSMS.Controllers.Account;
using CSMS.Controllers.Main;
using CSMS.Models.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CSMS.Views
{
    public partial class UserControlBrand : UserControl
    {
        MainController mainController = new MainController();
        private string Id = "";
        public UserControlBrand()
        {
            InitializeComponent();
        }

        public void EmptyBox()
        {
            txtBrandName.Clear();
            cmbStatus.SelectedIndex = 0;
        }
        public void EmptyBoxOptions()
        {
            txtBrandNameOptions.Clear();
            cmbStatusOptions.SelectedIndex = 0;
            Id = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtBrandName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter brand name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatus.SelectedIndex == 0)
            {
                MessageBox.Show("Please select status.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                //data base insert
                MainBrand mainBrand = new MainBrand
                {
                    BrandName = txtBrandName.Text,
                    BrandStatus= cmbStatus.SelectedItem.ToString(),
                };

                mainController.ControllerAddBrand(mainBrand);
                EmptyBox();
            }


        }

        private void tpAddBrand_Enter(object sender, EventArgs e)
        {
            EmptyBox();
        }

        private void tbManageBrand_Enter(object sender, EventArgs e)
        {
            txtSearchBrandName.Clear();
            dgvBrand.Columns[0].Visible = false;
            dgvBrand.DataSource = mainController.DisplyAndSearch("SELECT * FROM Brand");
            lblTotal.Text = dgvBrand.Rows.Count.ToString();
        }

        private void txtSearchBrandName_TextChanged(object sender, EventArgs e)
        {
            dgvBrand.DataSource = mainController.DisplyAndSearch("SELECT * FROM Brand WHERE Brand_Name LIKE'%" + txtSearchBrandName.Text + "%'");
            lblTotal.Text = dgvBrand.Rows.Count.ToString();
        }


        private void btnChange_Click(object sender, EventArgs e)
        {
            if (Id == "")
            {
                MessageBox.Show("First select row from rable.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtBrandNameOptions.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter brand name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatusOptions.SelectedIndex == 0)
            {
                MessageBox.Show("Please select status.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                //database update
                mainController.ControllerUpdateBrand(txtBrandNameOptions.Text, cmbStatusOptions.SelectedItem.ToString(), Id);
                EmptyBox();
                tcBrand.SelectedTab = tpManageBrand;
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (Id == "")
            {
                MessageBox.Show("First select row from rable.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtBrandNameOptions.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter brand name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatusOptions.SelectedIndex == 0)
            {
                MessageBox.Show("Please select status.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you want to delete this brand ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    //data remove
                    mainController.ControllerDeleteBrand(Id);
                    EmptyBox();
                    tcBrand.SelectedTab = tpManageBrand;
                }
            }



        }

        private void tpOptions_Enter(object sender, EventArgs e)
        {
            if (Id == "")
                tcBrand.SelectedTab = tpManageBrand;
        }

        private void tpOptions_Leave(object sender, EventArgs e)
        {
            EmptyBoxOptions();
        }

        private void dgvBrand_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvBrand.Rows[e.RowIndex];
                Id = row.Cells[0].Value.ToString();
                txtBrandNameOptions.Text = row.Cells[1].Value.ToString();
                cmbStatusOptions.SelectedItem = row.Cells[2].Value.ToString();
                tcBrand.SelectedTab = tpOptions;
            }
        }



    }
}
