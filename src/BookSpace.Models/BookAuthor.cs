namespace BookSpace.Models
{
    public class BookAuthor
    {
        public string AuthorId { get; set; }
        public AuthorDBModel Author { get; set; }

        public string BookId { get; set; }
        public BookDBModel Book { get; set; }
    }
}
