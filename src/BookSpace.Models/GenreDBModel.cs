using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookSpace.Models
{
    public class GenreDBModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<BookDBModel> Books { get; set; }
    }
}
