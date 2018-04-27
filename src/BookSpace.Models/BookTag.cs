namespace BookSpace.Models
{
    public class BookTag
    {
        public string BookId { get; set; }
        public BookDBModel Book { get; set; }

        public string TagId { get; set; }
        public TagDBModel Tag { get; set; }
    }
}
