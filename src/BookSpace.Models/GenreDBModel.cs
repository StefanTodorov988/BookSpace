using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookSpace.Models
{
    public class GenreDBModel
    {
        public string GenreId { get; set; }

        public string  Name { get; set; }

        public ICollection<BookGenre> GenreBooks { get; set; }
    }
}
