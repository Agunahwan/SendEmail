using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using System.Configuration;

namespace SendEmail
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            SendEmail();
        }

        public void SendEmail()
        {
            string from = txtFrom.Text;
            string to = txtTo.Text;
            string subject = txtSubject.Text;
            string body = txtBody.Text;
            int port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            string host = ConfigurationManager.AppSettings["SmtpHost"];
            string username = ConfigurationManager.AppSettings["SmtpUsername"];
            string password = ConfigurationManager.AppSettings["SmtpPassword"];

            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(from);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = port;
                smtp.Host = host; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(username, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
