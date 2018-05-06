using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpace.Web.Services.SmtpService.Contract
{
    public interface ISmtpSender
    {
        void SendMail(string mail, string message);
    }
}
