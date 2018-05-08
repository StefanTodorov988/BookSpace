using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpace.Web.Models.CommentViewModels
{
    public class DeleteCommentViewModel
    {
        public string CommentId { get; set; }

        public string BookId { get; set; }
    }
}
