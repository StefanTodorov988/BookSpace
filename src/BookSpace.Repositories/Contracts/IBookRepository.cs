using System.Collections.Generic;
using System.Threading.Tasks;
using BookSpace.Data.Contracts;
using BookSpace.Models;

namespace BookSpace.Repositories.Contracts
{
    public interface IBookRepository : IRepository<BookDBModel>
    {
        BookDBModel GetBookByNameAsync(string name);
        IEnumerable<BookDBModel> GetPageOfBooksAsync(int skip, int take);
        AuthorDBModel GetBookAuthorAsync(int bookId);
        GenreDBModel GetBookGenreAsync(int bookId);
        IEnumerable<CommentDBModel> GetBookCommentsAsync(int bookId);
        IEnumerable<TagDBModel> GetBookTagsAsync(int bookId);

        Task CreateBookAsync(BookDBModel book);
        Task UpdateBookAsync(BookDBModel book);
        Task RemoveBookAync(int bookId);
    }
}
