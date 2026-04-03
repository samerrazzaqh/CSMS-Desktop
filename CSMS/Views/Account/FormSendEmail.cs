using CSMS.Controllers.Account;
using CSMS.Models.Account;
using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace CSMS.Views.Account
{
    public partial class FormSendEmail : Form
    {
        AccountController accountController = new AccountController();
        string randomCode;
        public static string to;
        public FormSendEmail()
        {
            InitializeComponent();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.ShowDialog();
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            FormRegister formRegister = new FormRegister(); 
            formRegister.ShowDialog();
            Close();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Email  ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                var login = accountController.ControllerGetEmail(txtEmail.Text);

                if (login != null)
                {
                    string from, pass, messageBody;
                    Random rand = new Random();
                    randomCode = (rand.Next(999999)).ToString();
                    MailMessage message = new MailMessage();
                    to = (txtEmail.Text).ToString();
                    from = "samerrazzaqt@gmail.com";
                    pass = "zylphnicdnljxdge";
                    messageBody = "your reset code is " + randomCode;
                    message.To.Add(to);
                    message.From = new MailAddress(from);
                    message.Body = messageBody;
                    message.Subject = "password reseting code";
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    smtp.EnableSsl = true;
                    smtp.Port = 587;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(from, pass);
                    try
                    {
                        smtp.Send(message);
                        MessageBox.Show("code send successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Not found Email");
                }
            }



        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (txtVerCode.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Code  ", " Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (randomCode == (txtVerCode.Text).ToString())
            {   
                
                FormResetPassword formResetPassword = new FormResetPassword(txtEmail.Text.ToString());
                formResetPassword.ShowDialog();
                txtEmail.Clear();
                txtVerCode.Clear();
                Close();
            }
            else
            {
                MessageBox.Show("Wrong code");
            }


        }








    }
}
