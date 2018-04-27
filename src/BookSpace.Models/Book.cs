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

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<BookUser> BookUsers { get; set; }

        public virtual ICollection<BookGenre> BookGenres { get; set; }

        public virtual ICollection<BookAuthor> BookAuthors { get; set; }

        public virtual ICollection<BookTag> BookTags { get; set; }
    }
}
