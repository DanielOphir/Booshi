﻿using System;
using System.Net.Mail;

namespace BooshiWebApi.Services
{
    public class MailService
    {

        public bool SendMail(string to, string header, string body)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress("booshidelivery@gmail.com", "Booshi", System.Text.Encoding.UTF8);
            mail.Subject = header;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("booshidelivery@gmail.com", "Danielci11");
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