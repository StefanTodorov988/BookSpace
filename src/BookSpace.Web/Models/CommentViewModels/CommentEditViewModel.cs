using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpace.Web.Models.CommentViewModels
{
    public class CommentEditViewModel
    {
        public string CommentId { get; set; }

        public string BookId { get; set; }

        public string Value { get; set; }
    }
}
