namespace BookSpace.Models
{
    public class BookTag
    {
        public string BookId { get; set; }
        public virtual Book Book { get; set; }

        public string TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
