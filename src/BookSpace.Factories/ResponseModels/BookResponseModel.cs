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

        public string Genres { get; set; }

        public string Tags { get; set; }
    }
}
