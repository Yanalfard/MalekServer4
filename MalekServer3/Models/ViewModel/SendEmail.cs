﻿using System.Net.Mail;

namespace MalekServer3.Models.ViewModel
{
    public class SendEmail
    {
        public static void Send(string To, string Subject, string Body)
        {
            MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpClient SmtpServer = new SmtpClient();
            mail.From = new MailAddress("noreplaymalekserver1@gmail.com", "ملک سرور");
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;



            SmtpServer.Host = "smtp.gmail.com";
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("noreplaymalekserver1@gmail.com", "M09190995384s");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);



        }
    }
}