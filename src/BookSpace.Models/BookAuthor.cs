namespace BookSpace.Models
{
    public class BookAuthor
    {
        public string AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public string BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
