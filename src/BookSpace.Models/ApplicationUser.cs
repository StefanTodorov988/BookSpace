using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BookSpace.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string ProfilePictureUrl { get; set; }

<<<<<<< HEAD
        public UserAccessControlDBModel UserAccessControl { get; set; }

        public ICollection<BookUser> Books { get; set; }

        public ICollection<CommentDBModel> Comments { get; set; }
=======
        public UserAccessControl UserAccessControl { get; set; }

        public ICollection<BookUser> BookUsers { get; set; }

        public ICollection<Comment> Comments { get; set; }
>>>>>>> 280e0ded4b43c1723fcd4027699ec9ba290e71ec
    }
}
