using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookSpace.Data;
using BookSpace.Data.Contracts;
using BookSpace.Models;

namespace BookSpace.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(IDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Book>> GetBooksByTagAsync(string tag)
        {
            var books = await this.GetManyToManyAsync(g => g.Value.Contains(tag),
                                                      b => b.TagBooks,
                                                      x => x.Book
            );

            if (books == null)
            {
                throw new ArgumentNullException(nameof(books));
            }

            return books;
        }

        public async Task<Tag> GetTagByNameAsync(string tag)
        {
            return await this.GetAsync(b => b.Value == tag);
        }
    }
}
