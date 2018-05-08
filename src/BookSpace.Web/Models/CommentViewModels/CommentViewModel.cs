using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpace.Web.Models.CommentViewModels
{
    public class CommentViewModel 
    {
        public string CommentId { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public string Author { get; set; }

        public string UserId { get; set; }

        public bool CanEdit { get; set; }

        public string BookId { get; set; }
    }
}
