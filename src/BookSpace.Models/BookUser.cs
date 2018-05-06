using BookSpace.Models.Enums;

namespace BookSpace.Models
{
    public class BookUser
    {
        public string BookId { get; set; }
        public Book Book { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public BookState State { get; set; }

        public bool HasRatedBook { get; set; }

        public int Rate { get; set; }
    }
}
