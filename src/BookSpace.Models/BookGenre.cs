namespace BookSpace.Models
{
    public class BookGenre
    {
        public string BookId { get; set; }
        public BookDBModel Book { get; set; }

        public string GenreId { get; set; }
        public GenreDBModel Genre { get; set; }
    }
}
