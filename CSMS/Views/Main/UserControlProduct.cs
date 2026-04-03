using CSMS.Controllers.Main;
using CSMS.Models.Main;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CSMS.Views.Main
{
    public partial class UserControlProduct : UserControl
    {
        MainController mainController = new MainController();
        private string id = "";
        ImageConverter imageConverter;
        byte[] image;
        MemoryStream memoryStream;

        public UserControlProduct()
        {
            InitializeComponent();
            imageConverter = new ImageConverter();  
        }

        private void ImageUpload(PictureBox picture)
        {
            try
            {
                if(openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    picture.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Image upload error !","Error" ,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void EmptyBox()
        {
            var NameBrand = mainController.BrandCategoryProduct("SELECT Brand_Name FROM Brand WHERE Brand_Status = 'Available' ORDER BY Brand_Name;");
            var NameCategory = mainController.BrandCategoryProduct("SELECT Category_Name FROM Category WHERE Category_Status = 'Available' ORDER BY Category_Name;");
         
            
            txtProductName.Clear();
            picPhoto.Image = null;
            nudRate.Value = 0;
            nudQuantity.Value = 0;
            //cmbBrand.Items.Clear();
            // cmbBrand.Items.Insert(-1,"-- SELECT --");
            cmbBrand.DataSource = NameBrand; 
            cmbBrand.DisplayMember = "Brand_Name";
            cmbBrand.SelectedIndex = 0;
            //cmbCategory.Items.Clear();
            //cmbCategory.Items.Add("-- SELECT --");
            cmbCategory.DataSource = NameCategory;
            cmbCategory.DisplayMember = "Category_Name";
            cmbCategory.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;  

        }

        private void EmptyBox1()
        {
            txtProdectName1.Clear();
            picPhoto1.Image = null;
            nudRate1.Value = 0;
            nudQuantiy1.Value = 0;
            ComboBoxAutoFill();
            cmbStatus1.SelectedIndex = 0;
            id = "";
        }
        private void ComboBoxAutoFill()
        {
            var NameBrand = mainController.BrandCategoryProduct("SELECT Brand_Name FROM Brand WHERE Brand_Status = 'Available' ORDER BY Brand_Name;");
            var NameCategory = mainController.BrandCategoryProduct("SELECT Category_Name FROM Category WHERE Category_Status = 'Available' ORDER BY Category_Name;");

            //cmbBrand1.Items.Clear();
            //cmbBrand1.Items.Add("-- SELECT --");
            cmbBrand1.DataSource = NameBrand;
            cmbBrand1.DisplayMember = "Brand_Name";
            cmbBrand1.SelectedIndex = 0;
            //cmbCategory1.Items.Clear();
            //cmbCategory1.Items.Add("-- SELECT --");
            cmbCategory1.DataSource = NameCategory;
            cmbCategory1.DisplayMember = "Category_Name";
            cmbCategory1.SelectedIndex = 0;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            ImageUpload(picPhoto);
        }

        private void btnBrowse1_Click(object sender, EventArgs e)
        {
            ImageUpload(picPhoto1);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter product name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (picPhoto.Image == null)
            {
                MessageBox.Show("Please select image.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudRate.Value == 0)
            {
                MessageBox.Show("Please enter rate.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudQuantity.Value == 0)
            {
                MessageBox.Show("Please enter quantiy.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbBrand.SelectedIndex == 0)
            {
                MessageBox.Show("Please select brand.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbCategory.SelectedIndex == 0)
            {
                MessageBox.Show("Please select category.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MainProduct mainProduct = new MainProduct
                {
                    ProductName = txtProductName.Text,
                    ProductImage = (byte[])imageConverter.ConvertTo(picPhoto.Image, typeof(byte[])),
                    ProductRate = Convert.ToInt32(nudRate.Value),
                    ProductQuantity = Convert.ToInt32(nudQuantity.Value),
                    ProductBrand = cmbBrand.Text.ToString(),
                    ProductCategory = cmbCategory.Text.ToString(),
                    ProductStatus =  cmbStatus.SelectedItem.ToString()

                };

                mainController.ControllerAddProduct(mainProduct);
                EmptyBox();
            }

        }

        private void tpAddProduct_Enter(object sender, EventArgs e)
        {
            EmptyBox();
        }

        private void tpManageProduct_Enter(object sender, EventArgs e)
        {
            txtSearchProductName.Clear();
            dgvProduct.Columns[0].Visible = false;
            dgvProduct.DataSource = mainController.DisplyAndSearch("SELECT * FROM Product;");
            lblTotal.Text = dgvProduct.Rows.Count.ToString();
        }

        private void txtSearchProductName_TextChanged(object sender, EventArgs e)
        {
            dgvProduct.DataSource = mainController.DisplyAndSearch("SELECT * FROM Product WHERE Product_Name LIKE'%" + txtSearchProductName.Text + "%'");
            lblTotal.Text = dgvProduct.Rows.Count.ToString();
        }

        private void dgvProduct_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                ComboBoxAutoFill();
                DataGridViewRow row = dgvProduct.Rows[e.RowIndex];
                id = row.Cells[0].Value.ToString();
                txtProdectName1.Text = row.Cells[1].Value.ToString();
                image = (byte[])row.Cells[2].Value;
                memoryStream = new MemoryStream(image);
                picPhoto1.Image = Image.FromStream(memoryStream);
                nudRate1.Value = Convert.ToInt32(row.Cells[3].Value.ToString());
                nudQuantiy1.Value = Convert.ToInt32(row.Cells[4].Value.ToString());
                cmbBrand1.SelectedItem = row.Cells[5].Value.ToString();
                cmbCategory1.SelectedItem = row.Cells[6].Value.ToString();
                cmbStatus1.SelectedItem = row.Cells[6].Value.ToString();
                tcProduct.SelectedTab = tpOptions;

            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Please select row from table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtProdectName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter product name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (picPhoto1.Image == null)
            {
                MessageBox.Show("Please select image.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudRate1.Value == 0)
            {
                MessageBox.Show("Please enter rate.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudQuantiy1.Value == 0)
            {
                MessageBox.Show("Please enter quantiy.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbBrand1.Text == "")
            {
                MessageBox.Show("Please select brand.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbCategory1.Text == "")
            {
                MessageBox.Show("Please select category.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatus1.SelectedIndex == 0)
            {
                MessageBox.Show("Please select status.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                mainController.ControllerUpdateProduct(txtProdectName1.Text.Trim(), 
                    (byte[])imageConverter.ConvertTo(picPhoto1.Image, typeof(byte[])), 
                    Convert.ToInt32(nudRate1.Value), 
                    Convert.ToInt32(nudQuantiy1.Value),
                    cmbBrand1.Text.ToString(),
                    cmbCategory1.Text.ToString(),
                    cmbStatus1.SelectedItem.ToString(),
                    id);

                EmptyBox1();
                tcProduct.SelectedTab = tpManageProduct;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Please select row from table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            } 
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you want to delete this Product ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    //Computer.Computer.RemoveProduct(id);
                    mainController.ControllerDeleteProduct(id);
                    EmptyBox1();
                    tcProduct.SelectedTab = tpManageProduct;
                }
                
            }
        }

        private void tpOptions_Enter(object sender, EventArgs e)
        {
            if(id == "")
                tcProduct.SelectedTab = tpManageProduct;
        }

        private void tpOptions_Leave(object sender, EventArgs e)
        {
            EmptyBox1 ();
        }
    }
}
