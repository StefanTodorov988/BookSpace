using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookSpace.Models
{
    public class AuthorDBModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual ICollection <BookDBModel> Books { get; set; }
    }
}
