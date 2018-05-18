using BookSpace.Models;
using BookSpace.Repositories.Contracts;
using BookSpace.Web.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpace.Web.Logic.Core.Strategy
{
    public class TitleSearchStrategy : ISearchStrategy
    {
        private readonly IBookRepository _bookRepository;

        public TitleSearchStrategy(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<Book>> GetSearchedBook(string filter)
        {
            var result = await _bookRepository.Search(x => x.Title.Contains(filter));
            return result.ToList();
        }
    }
}
