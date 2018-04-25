using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookSpace.Models
{
    public class BookDBModel
    {
        private ICollection<AuthorDBModel> authors;
        private ICollection<GenreDBModel> genres;
        private ICollection<CommentDBModel> comments;
        private ICollection<ApplicationUser> userCollection;

        public BookDBModel()
        {
            this.authors = new HashSet<AuthorDBModel>();
            this.genres = new HashSet<GenreDBModel>();
            this.comments = new HashSet<CommentDBModel>();
            this.userCollection = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }

        public string ISBN { get; set; }
        
        [Required]
        public string Name { get; set; }

        public DateTime? PublicationYear { get; set; }

        [Required]
        public virtual ICollection<AuthorDBModel> Authors { get => authors; set => authors = value; }

        public virtual ICollection<GenreDBModel> Genres { get => genres; set => genres = value; }

        public virtual ICollection<CommentDBModel> Comments { get => comments; set => comments = value; }

        public virtual ICollection<ApplicationUser> UserCollection { get => userCollection; set => userCollection = value; }

        public decimal Rating { get; set; }

        public virtual CoverDBModel Cover { get; set; }


    }
}
