using BookSpace.Web.Models.BookViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpace.Web.Models
{
    public class HomePageViewModel
    {
        public BookOfTheDayViewModel BookOfTheDay { get; set; }
        public IEnumerable<PopularBookViewModel> PopularBooks { get; set; }
        public IEnumerable<NewBookViewModel> NewBooks { get; set; }
        public IEnumerable<TagViewModel> Tags { get; set; }
    }
}
