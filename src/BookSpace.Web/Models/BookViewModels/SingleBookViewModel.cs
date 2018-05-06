namespace BookSpace.Web.Models.BookViewModels
{
    public class SingleBookViewModel
    {
        public BookViewModel Book { get; set; }
        public BookPropertiesViewModel Properties { get; set; }
        public bool IsRated { get; set; }
        public int UserRating { get; set; }
    }
}
