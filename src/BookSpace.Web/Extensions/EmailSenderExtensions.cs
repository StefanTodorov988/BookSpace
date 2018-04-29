using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BookSpace.Web.Services;

<<<<<<< HEAD
namespace BookSpace.Web.Services
=======
namespace BookSpace.Web.Extensions
>>>>>>> 280e0ded4b43c1723fcd4027699ec9ba290e71ec
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
