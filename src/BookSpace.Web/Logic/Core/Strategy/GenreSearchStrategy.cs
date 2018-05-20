using BookSpace.Models;
using BookSpace.Repositories.Contracts;
using BookSpace.Web.Logic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpace.Web.Logic.Core.Strategy
{
    public class GenreSearchStrategy : ISearchStrategy
    {
        private readonly IGenreRepository _genreRepository;

        public GenreSearchStrategy(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<Book>> GetSearchedBook(string filter)
        {
            var result = await _genreRepository.GetBooksByGenreNameAsync(filter);
            return result.ToList();
        }
    }
}
