using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpace.Web.Models.CommentsViewModel
{
    public class CommentViewModel
    {
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
    }
}
