using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSpace.Data;
using BookSpace.Data.Contracts;
using BookSpace.Models;
using BookSpace.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookSpace.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(IDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Book>> GetBooksWithTagAsync(string tagId)
        {
            var books = await this.GetManyToManyAsync(g => g.TagId == tagId,
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
