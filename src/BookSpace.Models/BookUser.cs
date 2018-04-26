namespace BookSpace.Models
{
    public class BookUser
    {
        public string BookId { get; set; }
        public BookDBModel Book { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public bool? IsRead { get; set; }
    }
}
