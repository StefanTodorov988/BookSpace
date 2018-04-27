using System.Collections.Generic;

namespace BookSpace.Models
{
    public class Author
    {
        public string AuthorId { get; set; }

        public string Name { get; set; }

        public ICollection<BookAuthor> AuthorBooks { get; set; }

    }
}
