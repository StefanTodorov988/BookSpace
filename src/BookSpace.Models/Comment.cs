using System;
using System.Collections.Generic;

namespace BookSpace.Models
{
    public class Comment
    {
        public string CommentId { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public string BookId { get; set; }
        public virtual Book Book { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
