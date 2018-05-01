using System;
using System.Collections.Generic;

namespace BookSpace.Web.Areas.Book.Models
{
    public class DetailedBookViewModel
    {
        public string BookId { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public DateTime PublicationYear { get; set; }
        public decimal Rating { get; set; }
        public string CoverUrl { get; set; }
        public ICollection<string> Comments { get; set; }
        public ICollection<string> BookUsers { get; set; }
        public ICollection<string> BookGenres { get; set; }
        public ICollection<string> BookAuthors { get; set; }
        public ICollection<string> BookTags { get; set; }
    }
}
