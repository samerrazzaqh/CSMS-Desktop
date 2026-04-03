using CSMS.Controllers.Account;
using CSMS.Controllers.Main;
using CSMS.Models.Account;
using CSMS.Models.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSMS.Views.Main
{
    public partial class UserControlUser : UserControl
    {
        private string id = "";
        AccountController accountController = new AccountController();
        public UserControlUser()
        {
            InitializeComponent();
        }
        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        private bool ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one lower case letter.";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one upper case letter.";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Password should not be lesser than 8 or greater than 15 characters.";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one numeric value.";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one special case character.";
                return false;
            }
            else
            {
                return true;
            }
        }
        public void EmptyBox()
        {
            txtUserName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
        }
        public void EmptyBox1()
        {
            txtUserName1.Clear();
            txtEmail1.Clear();
            id = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter UserName.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtEmail.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Email.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Password.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                //data base insert
                var login = accountController.ControllerGetEmail(txtEmail.Text);

                if (login != null)
                {
                    MessageBox.Show("Email already exists !");
                    return;
                }
                else if (isValidEmail(txtEmail.Text) == false)
                {
                    MessageBox.Show("Email is not Valid !");
                }
                else if (ValidatePassword(txtPassword.Text, out string ErrorMessage) == false)
                {
                    MessageBox.Show(ErrorMessage);
                }
                else
                {
                    Register register = new Register
                    {
                        UserName = txtUserName.Text,
                        Email = txtEmail.Text,
                        Password = BCrypt.Net.BCrypt.HashString(txtPassword.Text),
                    };

                    accountController.ControllerRegister(register);
                    EmptyBox();
                }
            }
        }

        private void tpAddUser_Enter(object sender, EventArgs e)
        {
            EmptyBox();
        }

        private void tpManageUser_Enter(object sender, EventArgs e)
        {
            txtSearchUserName.Clear();
            dgvUser.Columns[0].Visible = false;
            dgvUser.DataSource = accountController.DisplyAndSearch("SELECT * FROM users");
            lblTotal.Text = dgvUser.Rows.Count.ToString();
        }

        private void txtSearchUserName_TextChanged(object sender, EventArgs e)
        {
            dgvUser.DataSource = accountController.DisplyAndSearch("SELECT * FROM users WHERE Users_Name LIKE'%" + txtSearchUserName.Text + "%'");
            lblTotal.Text = dgvUser.Rows.Count.ToString();
        }

        private void dgvUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                var login = accountController.ControllerLogin(txtEmail.Text);

                DataGridViewRow row = dgvUser.Rows[e.RowIndex];
                id = row.Cells[0].Value.ToString();
                txtUserName1.Text = row.Cells[1].Value.ToString();
                txtEmail1.Text = row.Cells[2].Value.ToString();
                tcUser.SelectedTab = tpOptions;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("First select row from rable.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtUserName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter UserName.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtEmail1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Email.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                //data base insert
                var login = accountController.ControllerGetEmail(txtEmail.Text);

                if (login != null)
                {
                    MessageBox.Show("Email already exists !");
                    return;
                }
                else if (isValidEmail(txtEmail1.Text) == false)
                {
                    MessageBox.Show("Email is not Valid !");
                }
                else
                {
                    accountController.ControllerUpdateUser(txtUserName1.Text, txtEmail1.Text, id);
                    EmptyBox1();
                    tcUser.SelectedTab = tpManageUser;
                }
            }


        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("First select row from rable.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you want to delete this User ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    //data remove
                    accountController.ControllerDeleteUser(id);
                    EmptyBox1();
                    tcUser.SelectedTab = tpManageUser;
                }
            }
        }

        private void tpOptions_Enter(object sender, EventArgs e)
        {
            if (id == "")
                tcUser.SelectedTab = tpManageUser;
        }

        private void tpOptions_Leave(object sender, EventArgs e)
        {
            EmptyBox1();
        }
    }
}
