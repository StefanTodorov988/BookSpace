using BookSpace.Models;
using BookSpace.Repositories.Contracts;
using BookSpace.Web.Logic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpace.Web.Logic.Core.Strategy
{
    public class AuthorSearchStrategy : ISearchStrategy
    {
        private readonly IBookRepository _bookRepository;

        public AuthorSearchStrategy(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<Book>> GetSearchedBook(string filter)
        {
            var result = await _bookRepository.Search(x => x.Author.Contains(filter));
            return result.ToList();
        }
    }
}
