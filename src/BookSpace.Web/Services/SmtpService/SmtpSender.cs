using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Mail;
namespace BookSpace.Web.Services.SmtpService
{
    public class SmtpSender
    {
        private readonly SmtpClient smtpClient;
        private const string appEmail = "bookspaceteam@gmail.com";
        private const string appEmailPassword = "bookspace123";
        public SmtpSender()
        {
            var AuthenticationDetails = new NetworkCredential(appEmail, appEmailPassword);
            smtpClient = new SmtpClient()
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = AuthenticationDetails
            };
        }

        public void SendMail(string mail, string message)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(mail);
            msg.From = new MailAddress(appEmail);
            msg.Subject = "Message";
            msg.Body = message;
            smtpClient.Send(msg);
        }
    }
}
