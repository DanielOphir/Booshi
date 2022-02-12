using Microsoft.Extensions.Configuration;
using System;
using System.Net.Mail;

namespace BooshiWebApi.Services
{
    public class MailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public bool SendMail(string to, string header, string body)
        {
            string emailAdress = "";
            string password = "";
            try { 
            emailAdress = _configuration.GetSection("Email")["EmailAddress"];
            password = _configuration.GetSection("Email")["Password"];
                if (string.IsNullOrEmpty(emailAdress) || string.IsNullOrEmpty(password))
                    throw new ArgumentNullException();
            }
            catch {
                return false;
            }
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(emailAdress, "Booshi", System.Text.Encoding.UTF8);
            mail.Subject = header;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(emailAdress, password);
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
