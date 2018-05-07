using System.Collections.Generic;

namespace BookSpace.Web.Models.BookViewModels
{
    public class AllBooksViewModel
    {
        public IEnumerable<BooksIndexViewModel> Books {get; set;}
        public int BooksCount { get; set; }
    }
}
