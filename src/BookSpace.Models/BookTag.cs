namespace BookSpace.Models
{
    public class BookTag
    {
        public string BookId { get; set; }
        public Book Book { get; set; }

        public string TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
