using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Mail;
using BookSpace.Web.Services.SmtpService.Contract;

namespace BookSpace.Web.Services.SmtpService
{
    public class SmtpSender : ISmtpSender
    {
        private readonly SmtpClient smtpClient;
        private const string appEmail = "bookspaceteam@gmail.com";
        private const string appEmailPassword = "bookspace123";
        private const string host = "smtp.gmail.com";
        public SmtpSender()
        {
            var AuthenticationDetails = new NetworkCredential(appEmail, appEmailPassword);
            smtpClient = new SmtpClient()
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = host,
                EnableSsl = true,
                Credentials = AuthenticationDetails
            };
        }

        public void SendMail(string mail, string subject ,string message)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(mail);
            msg.From = new MailAddress(appEmail);
            msg.Subject = subject;
            msg.Body = message;
            smtpClient.Send(msg);
        }
    }
}
