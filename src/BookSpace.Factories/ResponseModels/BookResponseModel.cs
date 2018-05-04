using System;
using System.Collections.Generic;
using System.Text;

namespace BookSpace.Factories.ResponseModels
{
    public class BookResponseModel
    {
        public string BookId { get; set; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public DateTime? PublicationYear { get; set; }

        public decimal Rating { get; set; }

        public string CoverUrl { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        //TODO:FIGURE OUT HOW GENRES AND TAGS WILL BE ADDED TO DB MODEL
        //public ICollection<BookGenre> BookGenres { get; set; }

        //public ICollection<BookTag> BookTags { get; set; }
    }
}
