using BookSpace.Models;
using BookSpace.Repositories.Contracts;
using BookSpace.Web.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpace.Web.Logic.Core.Strategy
{
    public class TagSearchStrategy : ISearchStrategy
    {
        private readonly ITagRepository _tagRepository;

        public TagSearchStrategy(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<List<Book>> GetSearchedBook(string filter)
        {
            var result = await _tagRepository.GetBooksByTagAsync(filter);
            return result.ToList();
        }
    }
}
