using BookSpace.Models.Enums;

namespace BookSpace.Models
{
    public class BookUser
    {
        public string BookId { get; set; }
        public virtual Book Book { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public BookState State { get; set; }
    }
}
