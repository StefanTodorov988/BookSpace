using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookSpace.Models
{
    public class CommentDBModel
    {
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public virtual BookDBModel Book { get; set; }
    }
}
