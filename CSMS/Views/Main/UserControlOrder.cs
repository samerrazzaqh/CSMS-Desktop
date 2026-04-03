using CSMS.Controllers.Main;
using CSMS.Models.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSMS.Views.Main
{
    public partial class UserControlOrder : UserControl
    {
        MainController mainController = new MainController();
        private string id = "";

        public UserControlOrder()
        {
            InitializeComponent();
        }
         public  void EmptyBox()
        {
            dtpDate.Value = DateTime.Now;
            txtCustomerName.Clear();
            mtbCustomerNumber.Clear();
            AddClear();
            dgvProductList.Rows.Clear();
            txtTotalAmount.Text = "0";
            nudPaidAmount.Value = 0;
            txtDueAmount.Text = "0";
            nudDiscount.Value = 0;
            txtGrandTotal.Text = "0";
            cmbPaymentStatus.SelectedIndex = 0;
        }

        private void AddClear()
        {
            var NameProduct = mainController.Product("SELECT Product_Name FROM Product WHERE Product_Status = 'Available' ORDER BY Product_Name;");
            var RateProduct = mainController.GetProductRate("SELECT Product_Rate FROM Product WHERE Product_Name LIKE'%" + cmbProduct.Text + "%' ORDER BY Product_Name;", "Product_Rate");
            //cmbProduct.Items.Clear();
            //cmbProduct.Items.Add("-- SELECT --");
            //Computer.Computer.BrandCategoryProduct("SELECT Product_Name FROM Product WHERE Product_Status = 'Available' ORDER BY Product_Name;",cmbProduct);
            txtRate.Clear();
            cmbProduct.DataSource = NameProduct;
            cmbProduct.DisplayMember = "Product_Name";
            txtRate.Text = RateProduct;
            cmbProduct.SelectedIndex = 0;
            nudQuantity.Value = 0;
            txtTotal.Text = "0";
        }

        private void EmptyBox1()
        {
            dtpDate.Value = DateTime.Now;
            txtCustomerName1.Clear();
            mtbCustomerNumber1.Clear();
            txtTotalAmount1.Text = "0";
            nudPaidAmount1.Value = 0;
            txtDueAmount1.Text = "0";
            nudDiscount1.Value = 0;
            txtGrandTotal1.Text = "0";
            cmbPaymentStatus1.SelectedIndex = 0;
            id = "";
        }

        RichTextBox richTextBox = new RichTextBox();
        private void Receipt()
        {
            richTextBox.Clear();
            richTextBox.Text += "\t         Computer Shop Management System\n";
            richTextBox.Text += "****************************************************\n\n";
            richTextBox.Text += "  Date: " +dtpDate.Text + "\n";
            richTextBox.Text += "  Name: " +txtCustomerName.Text.Trim() + "\n";
            richTextBox.Text += "  Number: " +mtbCustomerNumber.Text.Trim() + "\n\n";
            richTextBox.Text += "****************************************************\n\n";
            richTextBox.Text += "  Name\t\tRate\t\tQuantity\t\tTotal\n";
            for (int i = 0; i <dgvProductList.Rows.Count;i++)
            {
                for (int j = 0; j < dgvProductList.Columns.Count -1; j++)
                    richTextBox.Text += dgvProductList.Rows[i].Cells[j].Value.ToString() + "\t\t";
                richTextBox.Text += "\n";
            }
            richTextBox.Text += "****************************************************\n\n";
            richTextBox.Text += "\t\t\t\t\tTotal: $" +txtTotalAmount.Text + "\n";
            richTextBox.Text += "\t\t\t\t\tPaid Amount: $" + nudPaidAmount.Value + "\n";
            richTextBox.Text += "\t\t\t\t\tDue Amount: $" + txtDueAmount.Text + "\n";
            richTextBox.Text += "\t\t\t\t\tDiscount: $" + nudDiscount.Value + "\n";
            richTextBox.Text += "\t\t\t\t\tGrand Total: $" + txtGrandTotal.Text + "\n";
        }
        int oTotal = 0;
        private void btnAddPlus_Click(object sender, EventArgs e)
        {
            if (cmbProduct.Text == "")
            {
                MessageBox.Show("Please enter product.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudQuantity.Value == 0)
            {
                MessageBox.Show("Please select Quantity.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (nudQuantity.Value > 0)
                {
                    int rate, total;
                    Int32.TryParse(txtRate.Text,out rate);
                    Int32.TryParse(txtTotal.Text, out total);
                    if (dgvProductList.Rows.Count != 0)
                    {
                        foreach (DataGridViewRow rows in dgvProductList.Rows)
                        {
                            if (rows.Cells[0].Value.ToString() == cmbProduct.Text)
                            {
                                int quantity = Convert.ToInt32(rows.Cells[2].Value.ToString());
                                int total1 = Convert.ToInt32(rows.Cells[3].Value.ToString());
                                quantity += Convert.ToInt32(nudQuantity.Value);
                                total1 += total;
                                rows.Cells[2].Value = quantity;
                                rows.Cells[3].Value = total1;
                                AddClear();
                            }
                        }

                        if (cmbProduct.SelectedIndex != 0)
                        {
                            txtTotal.Text = (rate * Convert.ToInt32(nudQuantity.Value)).ToString();
                            string[] row =
                            {
                               cmbProduct.Text,txtRate.Text,nudQuantity.Value.ToString(),txtTotal.Text
                            };
                            dgvProductList.Rows.Add(row);
                            AddClear();
                        }
                    }
                    else
                    {
                        txtTotal.Text = (rate * Convert.ToInt32(nudQuantity.Value)).ToString();
                        string[] row =
                        {
                                        cmbProduct.Text,txtRate.Text,nudQuantity.Value.ToString(),txtTotal.Text
                        };
                        dgvProductList.Rows.Add(row);
                        AddClear();
                    }
                }
                txtTotalAmount.Text = oTotal.ToString();
            }
            foreach(DataGridViewRow rows in dgvProductList.Rows)
            {
                oTotal += Convert.ToInt32(rows.Cells[3].Value.ToString());
                txtTotalAmount.Text = oTotal.ToString();
            }
            oTotal = 0;

        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
           var RateProduct = mainController.GetProductRate("SELECT Product_Rate FROM Product WHERE Product_Name LIKE'%" + cmbProduct.Text + "%' ORDER BY Product_Name;", "Product_Rate");
            txtRate.Clear();
            string rate = RateProduct;
            if (rate != string.Empty)
                txtRate.Text = rate;

        }

        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
            int rate;
            Int32.TryParse(txtRate.Text,out rate);
            txtTotal.Text = (rate * Convert.ToInt32(nudQuantity.Value)).ToString();
        }

        private void dgvProductList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                int rowIndex = dgvProductList.CurrentCell.RowIndex;
                dgvProductList.Rows.RemoveAt(rowIndex);
                if (dgvProductList.Rows.Count !=0)
                {
                    foreach (DataGridViewRow rows in dgvProductList.Rows)
                    {
                        oTotal += Convert.ToInt32(rows.Cells[3].Value.ToString());
                        txtTotalAmount.Text = oTotal.ToString();
                    }
                }
                else
                {
                    txtTotalAmount.Text ="0";
                    oTotal = 0;
                }
            }
        }

        private void nudPaidAmount_ValueChanged(object sender, EventArgs e)
        {
            txtDueAmount.Text = (Convert.ToInt32(nudPaidAmount.Value) - Convert.ToInt32(txtTotalAmount.Text)).ToString(); 
        }

        private void nudDiscount_ValueChanged(object sender, EventArgs e)
        {
            txtGrandTotal.Text = (Convert.ToInt32(txtTotalAmount.Text) - Convert.ToInt32(nudDiscount.Value)).ToString();
        }

        private void txtTotalAmount_TextChanged(object sender, EventArgs e)
        {
            nudPaidAmount_ValueChanged(sender,e);
            nudDiscount_ValueChanged(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text.Trim()==string.Empty)
            {
                MessageBox.Show("Please enter customer name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (!mtbCustomerNumber.MaskCompleted)
            {
                MessageBox.Show("Please enter valid customer number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudPaidAmount.Value == 0)
            {
                MessageBox.Show("Please enter paid amount.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbPaymentStatus.SelectedIndex == 0)
            {
                MessageBox.Show("Please select Payment Status.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                //data base insert
                MainOrder mainOrder = new MainOrder
                {
                    OrdersDate = dtpDate.Value.Date,
                    CustomerName = txtCustomerName.Text.Trim(),
                    CustomerNumber = mtbCustomerNumber.Text,
                    TotalAmount = Convert.ToInt32(txtTotalAmount.Text),
                    PaidAmount = Convert.ToInt32(nudPaidAmount.Value),
                    DueAmount = Convert.ToInt32(txtDueAmount.Text),
                    Discount = Convert.ToInt32(nudDiscount.Value),
                    GrandTotal = Convert.ToInt32(txtGrandTotal.Text),
                    PaymentStatus = cmbPaymentStatus.Text

                };

                mainController.ControllerAddOrder(mainOrder);
                EmptyBox();
            }
        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            Receipt();
            printPreviewDialog.Document = printDocument;
            printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm",285,600);
            printPreviewDialog.ShowDialog();
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox.Text,new Font("Segoe UI",6,FontStyle.Regular),Brushes.Black,new Point(10,10));
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            nudQuantity_ValueChanged(sender, e);
        }

        private void tpManageOrder_Enter(object sender, EventArgs e)
        {
            

            txtSearchCustomerName.Clear();
            dgvOrders.Columns[0].Visible = false;
            dgvOrders.DataSource = mainController.DisplyAndSearch("SELECT * FROM Orders;");
            lblTotal.Text = dgvOrders.Rows.Count.ToString();
        }

        private void txtSearchCustomerName_TextChanged(object sender, EventArgs e)
        {
            dgvOrders.DataSource = mainController.DisplyAndSearch("SELECT * FROM Orders WHERE Customer_Name LIKE'%" + txtSearchCustomerName.Text + "%'");
            lblTotal.Text = dgvOrders.Rows.Count.ToString();
        }

        private void dgvOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvOrders.Rows[e.RowIndex];
                id = row.Cells[0].Value.ToString();
                dtpDate1.Text = row.Cells[1].Value.ToString();
                txtCustomerName1.Text = row.Cells[2].Value.ToString();
                mtbCustomerNumber1.Text = row.Cells[3].Value.ToString();
                txtTotalAmount1.Text = row.Cells[4].Value.ToString();
                nudPaidAmount1.Value = Convert.ToInt32(row.Cells[5].Value.ToString());
                txtDueAmount1.Text = row.Cells[6].Value.ToString();
                nudDiscount1.Value = Convert.ToInt32(row.Cells[7].Value.ToString());
                txtGrandTotal1.Text = row.Cells[8].Value.ToString();
                cmbPaymentStatus1.SelectedItem = row.Cells[9].Value.ToString();
                tcOrder.SelectedTab = tpOptions;
            }
        }

        private void tpOptions_Enter(object sender, EventArgs e)
        {
            if (id == "")
                tcOrder.SelectedTab = tpManageOrder;
        }

        private void tpOptions_Leave(object sender, EventArgs e)
        {
            EmptyBox1();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("First select row from table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if(txtCustomerName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter customer name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (!mtbCustomerNumber1.MaskCompleted)
            {
                MessageBox.Show("Please enter valid customer number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudPaidAmount1.Value == 0)
            {
                MessageBox.Show("Please enter paid amount.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbPaymentStatus1.SelectedIndex == 0)
            {
                MessageBox.Show("Please select Payment Status.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                mainController.ControllerUpdateOrder(
                    dtpDate.Value.Date,
                    txtCustomerName1.Text.Trim(),
                    mtbCustomerNumber1.Text,
                    Convert.ToInt32(txtTotalAmount1.Text),
                    Convert.ToInt32(nudPaidAmount1.Value),
                    Convert.ToInt32(txtDueAmount1.Text),
                    Convert.ToInt32(nudDiscount1.Value),
                    Convert.ToInt32(txtGrandTotal1.Text),
                    cmbPaymentStatus1.SelectedItem.ToString(),
                    id);
                EmptyBox1();
                tcOrder.SelectedTab = tpManageOrder;
            }
        }

        private void tpAddOrder_Enter(object sender, EventArgs e)
        {
            EmptyBox();
        }

        private void nudPaidAmount1_ValueChanged(object sender, EventArgs e)
        {
            txtDueAmount1.Text = (Convert.ToInt32(nudPaidAmount1.Value) - Convert.ToInt32(txtTotalAmount1.Text)).ToString();
        }

        private void nudDiscount1_ValueChanged(object sender, EventArgs e)
        {
            txtGrandTotal1.Text = (Convert.ToInt32(txtTotalAmount1.Text) - Convert.ToInt32(nudDiscount1.Value)).ToString();
        }

        private void txtTotalAmount1_TextChanged(object sender, EventArgs e)
        {
            nudPaidAmount1_ValueChanged(sender, e);
            nudDiscount1_ValueChanged(sender, e);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("First select row from table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you want to delete this Order ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    mainController.ControllerDeleteOrder(id);
                    EmptyBox1();
                    tcOrder.SelectedTab = tpManageOrder;
                }

            }
        }











    }
}
