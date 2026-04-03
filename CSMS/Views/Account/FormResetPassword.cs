using CSMS.Controllers.Account;
using CSMS.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CSMS.Views.Account
{
    public partial class FormResetPassword : Form
    {
        AccountController accountController = new AccountController();
        string email;
        public FormResetPassword(string emailForm)
        {
            email = emailForm;
            InitializeComponent();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReast_Click(object sender, EventArgs e)
        {
             if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Password  ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtRePassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter RePassword  ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtPassword.Text != txtRePassword.Text)
            {
                MessageBox.Show("Please check Password  ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                string Password = BCrypt.Net.BCrypt.HashString(txtPassword.Text);
                accountController.ControllerUpdatePassword(email, Password);
                txtPassword.Clear();
                txtRePassword.Clear();
                FormLogin formLogin = new FormLogin();
                formLogin.ShowDialog();
                Close();

            }
        }

        private void lblLogin_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.ShowDialog();
            Close();
        }

        private void lblRegister_Click(object sender, EventArgs e)
        {
            FormRegister formRegister = new FormRegister();
            formRegister.ShowDialog();
            Close();
        }
    }
}
