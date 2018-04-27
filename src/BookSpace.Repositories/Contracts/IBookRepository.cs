using System.Collections.Generic;
using System.Threading.Tasks;
using BookSpace.Data.Contracts;
using BookSpace.Models;

namespace BookSpace.Repositories.Contracts
{
    public interface IBookRepository : IRepository<BookDBModel>
    {
        BookDBModel GetBookByTitleAsync(string title);
        IEnumerable<BookDBModel> GetPageOfBooksAsync(int skip, int take);
        IEnumerable<AuthorDBModel> GetBookAuthorsAsync(string bookId);
        IEnumerable<GenreDBModel> GetBookGenresAsync(string bookId);
        IEnumerable<CommentDBModel> GetBookCommentsAsync(string bookId);
        IEnumerable<TagDBModel> GetBookTagsAsync(string bookId);

        void CreateBookAsync(BookDBModel book);
        void UpdateBookAsync(BookDBModel book);
        void RemoveBookAync(string bookId);
    }
}
