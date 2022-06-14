using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagementSystem.Services
{
    
    public class EmailService
    {
        public void Send(string from, string to, string subject, string content)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = content };

            // send email
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.mail.yahoo.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("mengaji121@yahoo.com", "oypqfmgwbomycrzs");
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}