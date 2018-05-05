using BookSpace.Data.Contracts;
using BookSpace.Models;
using BookSpace.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSpace.Repositories
{
    public  interface IGenreRepository : IRepository<Genre>
    {
        Task<Genre> GetGenreByNameAsync(string name);
        Task<PagedResult<Book>> GetBooksByGenrePageAsync(string genreId, int page, int pageSize);
    }
}