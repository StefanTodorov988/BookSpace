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
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(IDbContext dbContext) : base(dbContext) { }


        public async Task<Book> GetBookByTitleAsync(string title)
        {
            return await this.GetAsync(b => b.Title == title);
        }

        public async Task<IEnumerable<Book>> GetPageOfBooksAscync(int take, int skip)
        {
            var pageRecords = await this.GetPaged(take, skip);

            return pageRecords.Results;
        }

        public async Task<IEnumerable<Genre>> GetBookGenresAsync(string bookId)
        {
            var genres = await this.GetManyToManyAsync(b => b.BookId == bookId,
                                                       bg => bg.BookGenres,
                                                       g => g.Genre);

            if (genres == null)
            {
                throw new ArgumentNullException(nameof(genres));
            }

            return genres;
        }

        public async Task<IEnumerable<Comment>> GetBookCommentsAsync(string bookId)
        {
            var comments = await this.GetOneToManyAsync(b => b.BookId == bookId,
                                                       bg => bg.Comments);

            if (comments == null)
            {
                throw new ArgumentNullException(nameof(comments));
            }

            return comments;
        }

        public async Task<IEnumerable<Tag>> GetBookTagsAsync(string bookId)
        {

            var tags = await this.GetManyToManyAsync(b => b.BookId == bookId,
                                                       bg => bg.BookTags,
                                                       g => g.Tag);

            if (tags == null)
            {
                throw new ArgumentNullException(nameof(tags));
            }

            return tags;
        }

  
        public async Task RemoveBookAync(string bookId)
        {
            var bookToRemove = await this.GetByIdAsync(bookId);
            await this.DeleteAsync(bookToRemove);
        }
    }
}
