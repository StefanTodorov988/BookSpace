using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookSpace.Models
{
    public class Book
    {
        public string BookId { get; set; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public DateTime? PublicationYear { get; set; }

        public decimal Rating { get; set; }

        public string CoverUrl { get; set; }

        public string Description { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<BookUser> BookUsers { get; set; }

        public ICollection<BookGenre> BookGenres { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }

        public ICollection<BookTag> BookTags { get; set; }
    }
}
