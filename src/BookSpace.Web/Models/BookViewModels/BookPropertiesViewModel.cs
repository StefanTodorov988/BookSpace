using BookSpace.Web.Models.CommentViewModels;
using System.Collections.Generic;

namespace BookSpace.Web.Models.BookViewModels
{
    public class BookPropertiesViewModel
    {
        public IEnumerable<CommentViewModel> Comments { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
