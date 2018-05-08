using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookSpace.Web.Models.BookViewModels
{
    public class CreateBookViewModel
    {
        
        public string BookId { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Title")]
        public string Title { get; set; }
        public string Isbn { get; set; }
        public DateTime PublicationYear { get; set; }
        public decimal Rating { get; set; }
        public string CoverUrl { get; set; }
        [Required]
        [Display(Name = "Author")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Author { get; set; }
        [Required]
        [Display(Name = "Description")]
        [StringLength(150, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Genres")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Genres { get; set; }
        [Required]
        [Display(Name = "Tags")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Tags { get; set; }
    }
}
