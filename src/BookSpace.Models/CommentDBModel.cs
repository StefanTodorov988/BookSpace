using System.Collections.Generic;

namespace BookSpace.Models
{
    public class CommentDBModel
    {
        public string CommentId { get; set; }

        public string Value { get; set; }

        public string BookId { get; set; }
        public BookDBModel Book { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
