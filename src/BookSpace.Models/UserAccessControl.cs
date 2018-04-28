using System;

namespace BookSpace.Models
{
    public class UserAccessControl
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime LastLogin { get; set; }

        public DateTime LockOutEndTime { get; set; }

        public DateTime BanEndTime { get; set; }
    }
}
