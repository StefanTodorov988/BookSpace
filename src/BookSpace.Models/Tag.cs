using System.Collections.Generic;

namespace BookSpace.Models
{
    public class Tag
    {
        public string TagId { get; set; }

        public string Value { get; set;  }

        public ICollection<BookTag> TagBooks { get; set; }
    }
}
