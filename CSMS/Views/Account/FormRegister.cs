using CSMS.Controllers.Account;
using CSMS.Models.Account;
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

namespace CSMS.Views.Account
{
    public partial class FormRegister : Form
    {
        AccountController accountController = new AccountController();
        public FormRegister()
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
            txtEmail.Clear();
            txtUserName.Clear();    
            txtPassword.Clear();
            txtRePassword.Clear();
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Email  ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtUserName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter UserName  ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtPassword.Text.Trim() == string.Empty)
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
                var login = accountController.ControllerGetEmail(txtEmail.Text);

                if (login != null)
                {
                    MessageBox.Show("Email already exists !");
                    return;
                }
                else if(isValidEmail(txtEmail.Text) == false)
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
                    FormLogin formLogin = new FormLogin();
                    formLogin.ShowDialog();
                    Close();
                }

                
            }


        }

        private void label6_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.ShowDialog();
            Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
