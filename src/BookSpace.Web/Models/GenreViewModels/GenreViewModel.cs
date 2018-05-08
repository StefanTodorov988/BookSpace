using System.ComponentModel.DataAnnotations;

namespace BookSpace.Web.Models.GenreViewModels
{
    public class GenreViewModel
    {
        public string GenreId { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Genre")]
        public string Name { get; set; }
    }
}
