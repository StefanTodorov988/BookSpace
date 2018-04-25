using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookSpace.Models
{
    public class CoverDBModel
    {
        //one to one. Id should be same as bookId
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
