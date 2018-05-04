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
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(IDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Book>> GetBooksWithGenreAsync(string genreId)
        {
            var books = await this.GetManyToManyAsync(g => g.GenreId == genreId,
                                                      b => b.GenreBooks,
                                                      x => x.Book
            );

            if (books == null)
            {
                throw new ArgumentNullException(nameof(books));
            }

            return books;
        }

        public async Task<Genre> GetGenreByNameAsync(string name)
        {
            return await this.GetAsync(b => b.Name == name);
        }
    }
}
