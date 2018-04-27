namespace BookSpace.Models
{
    public class BookAuthor
    {
        public string AuthorId { get; set; }
        public Author Author { get; set; }

        public string BookId { get; set; }
        public Book Book { get; set; }
    }
}
