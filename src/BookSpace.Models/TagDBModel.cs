using System.Collections.Generic;

namespace BookSpace.Models
{
    public class TagDBModel
    {
        public string TagId { get; set; }

        public ICollection<BookTag> TagBooks { get; set; }
    }
}
