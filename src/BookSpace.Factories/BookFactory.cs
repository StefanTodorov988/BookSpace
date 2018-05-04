using BookSpace.Factories.ResponseModels;
using BookSpace.Models;
using System;
using System.Threading.Tasks;

namespace BookSpace.Factories
{
    public class BookFactory : IFactory<Book, BookResponseModel>
    {

       //Book Create(string bookId, string isbn, string title, DateTime publicationYear, string coverUrl);
     
        public Book Create(BookResponseModel model)
        {
            return new Book()
            {
                BookId = Guid.NewGuid().ToString(),
                Isbn = model.Isbn,
                Title = model.Title,
                PublicationYear = model.PublicationYear,
                Rating = model.Rating,
                CoverUrl = model.CoverUrl,
                Description = model.Description,
                Author = model.Author
            };
        }
    }
}
