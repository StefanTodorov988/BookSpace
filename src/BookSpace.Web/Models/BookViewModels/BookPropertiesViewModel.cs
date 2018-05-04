using BookSpace.Web.Models.CommentsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpace.Web.Models.BookViewModels
{
    public class BookPropertiesViewModel
    {
        public IEnumerable<CommentViewModel> Comments { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
