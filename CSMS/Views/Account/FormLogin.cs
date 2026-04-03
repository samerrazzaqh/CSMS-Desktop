using CSMS.Controllers.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSMS.Views.Account
{
    public partial class FormLogin : Form
    {
        AccountController accountController = new AccountController();
        public FormLogin()
        {
            InitializeComponent();
        }
        public void EmptyBox()
        {
            txtEmail.Clear();
            txtPassword.Clear();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Email  ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Password  ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                var login = accountController.ControllerLogin(txtEmail.Text);

                if (login != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(txtPassword.Text, login.Password))
                    {
                        FormMain formMain = new FormMain();
                        formMain.username = login.Email;
                        formMain.ShowDialog();
                        EmptyBox();
                    }
                    else
                    {
                        MessageBox.Show("Password error");
                        EmptyBox();
                    }
                }
                else
                {
                    MessageBox.Show("Not found Email");
                    EmptyBox();
                }
            }
        }

        private void picShow_Click(object sender, EventArgs e)
        {
            if (picShow.Visible == true)
            {
                txtPassword.UseSystemPasswordChar = false;
                picShow.Visible = false;
                picHide.Visible = true;
            }
        }

        private void picHide_Click(object sender, EventArgs e)
        {
            if (picHide.Visible == true)
            {
                txtPassword.UseSystemPasswordChar = true;
                picShow.Visible = true;
                picHide.Visible = false;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            FormRegister formRegister = new FormRegister();
            formRegister.ShowDialog();
            Close();
        }

        private void FgetPassword_Click(object sender, EventArgs e)
        {
            FormSendEmail formSendEmail = new FormSendEmail();
            formSendEmail.ShowDialog();
            Close();
        }
    }
}
