using BookSpace.Web.Models.BookViewModels;
using System.Collections.Generic;

namespace BookSpace.Web.Areas.Admin.Models
{
    public class AllBooksAdminViewModel
    {
        public IEnumerable<ListBookViewModel> Books { get; set; }
        public int BooksCount { get; set; }
    }
}
