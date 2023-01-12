using Domain;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Data.Repositories
{
    public class EmailRepository : IlogRepository
    {
        public void Log(string Message, string IP, string UserName)
        {
          
            SmtpClient smtpclient = new SmtpClient("smtp.office365.com", 587);
            smtpclient.EnableSsl = true;
            smtpclient.Credentials = new NetworkCredential("epassignment@hotmail.com", "Buttface5");

       
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("epassignment@hotmail.com");
            mail.To.Add("epassignment@hotmail.com");
            mail.Subject = "Log";
            mail.Body = $"Message Received: {Message} \n IP Address: {IP} \n User: {UserName}";
            smtpclient.Send(mail);
        }

        public void Log(Exception exception, string IP, string UserName)
        {
       
            SmtpClient smtpclient = new SmtpClient("smtp.office365.com", 587);
            smtpclient.EnableSsl = true; 
            smtpclient.Credentials = new NetworkCredential("epassignment@hotmail.com", "Buttface5");

            //Instance of message
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("epassignment@hotmail.com");
            mail.To.Add("epassignment@hotmail.com");
            mail.Subject = "Log";
            mail.Body = $"Error: {exception} \n IP Address: {IP} \n User: {UserName}";
            smtpclient.Send(mail);
        }
    }
}
