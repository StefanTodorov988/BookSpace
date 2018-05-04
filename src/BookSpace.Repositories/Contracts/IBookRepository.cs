using System.Collections.Generic;
using System.Threading.Tasks;
using BookSpace.Data.Contracts;
using BookSpace.Models;

namespace BookSpace.Repositories.Contracts
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book> GetBookByTitleAsync(string title);
        Task<IEnumerable<Book>> GetPageOfBooksAscync(int take, int skip);
        Task<IEnumerable<Genre>> GetBookGenresAsync(string bookId);
        Task<IEnumerable<Comment>> GetBookCommentsAsync(string bookId);
        Task<IEnumerable<Tag>> GetBookTagsAsync(string bookId);

        Task RemoveBookAync(string bookId);
    }
}
