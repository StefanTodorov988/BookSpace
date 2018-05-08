using System.ComponentModel.DataAnnotations;

namespace BookSpace.Web.Models.BookViewModels
{
    public class TagViewModel
    {
        public string TagId { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Name")]
        public string Value { get; set; }
    }
}