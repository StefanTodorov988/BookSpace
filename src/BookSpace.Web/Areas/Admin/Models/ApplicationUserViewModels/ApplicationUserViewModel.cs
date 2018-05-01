using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpace.Web.Areas.Admin.Models.ApplicationUserViewModels
{
    public class ApplicationUserViewModel
    {

        public string Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string ProfilePictureUrl { get { return "http://www.personalbrandingblog.com/wp-content/uploads/2017/08/blank-profile-picture-973460_640-300x300.png"; }}

        public bool isAllowed { get; set; }

        public bool isAdmin { get; set; }

    }
}
