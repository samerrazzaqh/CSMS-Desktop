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

namespace CSMS.Views.Main
{
    public partial class UserControlCategory : UserControl
    {
        MainController mainController = new MainController();
        private string Id = "";
        public UserControlCategory()
        {
            InitializeComponent();
        }

        public void EmptyBox()
        {
            txtCategoryName.Clear();
            cmbStatus.SelectedIndex = 0;
        }
        public void EmptyBoxOptions()
        {
            txtCategoryNameOptions.Clear();
            cmbStatusOptions.SelectedIndex = 0;
            Id = "";
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtCategoryName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Category name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MainCategory mainCategory = new MainCategory
                {
                    CategoryName = txtCategoryName.Text,
                    CategoryStatus = cmbStatus.SelectedItem.ToString(),
                };

                mainController.ControllerAddCategory(mainCategory);
                EmptyBox();
            }
        }

        private void tpAddCategory_Enter(object sender, EventArgs e)
        {
            EmptyBox();
        }

        private void tpManageCategory_Enter(object sender, EventArgs e)
        {
            txtSearchCategoryName.Clear();
            dgvCategory.Columns[0].Visible = false;
            dgvCategory.DataSource = mainController.DisplyAndSearch("SELECT * FROM Category");
            lblTotal.Text = dgvCategory.Rows.Count.ToString();
        }

        private void txtSearchCategoryName_TextChanged(object sender, EventArgs e)
        {
            dgvCategory.DataSource = mainController.DisplyAndSearch("SELECT * FROM Category WHERE Category_Name LIKE'%" + txtSearchCategoryName.Text + "%'");
            lblTotal.Text = dgvCategory.Rows.Count.ToString();
        }

        private void dgvCategory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvCategory.Rows[e.RowIndex];
                Id = row.Cells[0].Value.ToString();
                txtCategoryNameOptions.Text = row.Cells[1].Value.ToString();
                cmbStatusOptions.SelectedItem = row.Cells[2].Value.ToString();
                tcCategory.SelectedTab = tpOptions;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (Id == "")
            {
                MessageBox.Show("First select row from rable.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtCategoryNameOptions.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Category name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                mainController.ControllerUpdateCategory(txtCategoryNameOptions.Text, cmbStatusOptions.SelectedItem.ToString(), Id);
                EmptyBox();
                tcCategory.SelectedTab = tpManageCategory;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (Id == "")
            {
                MessageBox.Show("First select row from rable.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtCategoryNameOptions.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Category name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatusOptions.SelectedIndex == 0)
            {
                MessageBox.Show("Please select status.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you want to delete this Category ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    //data remove
                    mainController.ControllerDeleteCategory(Id);
                    EmptyBox();
                    tcCategory.SelectedTab = tpManageCategory;
                }
            }
        }

        private void tpOptions_Enter(object sender, EventArgs e)
        {
            if (Id == "")
                tcCategory.SelectedTab = tpManageCategory;
        }

        private void tpOptions_Leave(object sender, EventArgs e)
        {
            EmptyBoxOptions();
        }
    }
}
