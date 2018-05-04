using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpace.Web.Models.BookViewModels
{
    public class UserBookViewModel
    {
        public string BookId { get; set; }
        public string Title { get; set; }
        public DateTime PublicationYear { get; set; }
        public decimal Rating { get; set; }
        public string CoverUrl { get; set; }
        public string Author { get; set; }
    }
}
