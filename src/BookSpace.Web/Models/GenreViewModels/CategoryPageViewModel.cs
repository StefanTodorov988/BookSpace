using BookSpace.Web.Models.BookViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpace.Web.Models.GenreViewModels
{
    public class CategoryPageViewModel
    {
        public GenreViewModel Genre { get; set; }
        public IEnumerable<BookByCategoryViewModel> Books { get; set; }
    }
}
