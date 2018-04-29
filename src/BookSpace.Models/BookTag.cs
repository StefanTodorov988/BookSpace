namespace BookSpace.Models
{
    public class BookTag
    {
        public string BookId { get; set; }
<<<<<<< HEAD
        public BookDBModel Book { get; set; }

        public string TagId { get; set; }
        public TagDBModel Tag { get; set; }
=======
        public Book Book { get; set; }

        public string TagId { get; set; }
        public Tag Tag { get; set; }
>>>>>>> 280e0ded4b43c1723fcd4027699ec9ba290e71ec
    }
}
