using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookSpace.Models
{
    public class BookDBModel
    {
        public string BookId { get; set; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public DateTime? PublicationYear { get; set; }

        public decimal Rating { get; set; }

        public string CoverUrl { get; set; }

        public ICollection<CommentDBModel> Comments { get; set; }

        public ICollection<BookUser> BookUsers { get; set; }

        public ICollection<BookGenre> BookGenres { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }

        public ICollection<BookTag> BookTags { get; set; }
    }
}
