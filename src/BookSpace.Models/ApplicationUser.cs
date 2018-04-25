using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BookSpace.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        private ICollection<BookDBModel> readBooks;
        private ICollection<BookDBModel> booksToRead;
        private ICollection<CommentDBModel> comments;

        public ApplicationUser()
        {
            this.readBooks = new HashSet<BookDBModel>();
            this.booksToRead = new HashSet<BookDBModel>();
        }

        public virtual ICollection<BookDBModel> ReadBooks { get => readBooks; set => readBooks = value; }

        public virtual ICollection<BookDBModel> BooksToRead { get => booksToRead; set => booksToRead = value; }

        public virtual ICollection<CommentDBModel> Comments { get => comments; set => comments = value; }

        public bool IsDeleted { get; set; }


    }
}
