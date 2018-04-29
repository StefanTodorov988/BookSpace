using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSpace.Data;
using BookSpace.Data.Contracts;
using BookSpace.Models;
using BookSpace.Repositories.Contracts;

namespace BookSpace.Repositories
{
    public class BookRepository :  BaseRepository<Book>, IBookRepository
    {
        public BookRepository(IDbContext dbContext) : base(dbContext) {}


        public async Task<Book> GetBookByTitleAsync(string title)
        {
            return await this.GetAsync(b => b.Title == title);
        }

        public async Task<IEnumerable<Book>> GetPageOfBooksAscync(int take, int skip)
        {
            var books = await this.GetAllAsync();

            return this.GetPaged(books, take, skip).Results;
        }

        public async Task<IEnumerable<Author>> GetBookAuthorsAsync(string bookId)
        {
            return await this.GetAsync(book => book.BookId == bookId)
                           .ContinueWith(
                                         b => b.Result.BookAuthors
                                                      .Select(ba => ba.Author)
                                        );
        }

        public async Task<IEnumerable<Genre>> GetBookGenresAsync(string bookId)
        {
            return await this.GetAsync(book => book.BookId == bookId)
                             .ContinueWith(
                                         b => b.Result.BookGenres
                                                      .Select(bg => bg.Genre)
                                        );
        }

        public async Task<IEnumerable<Comment>> GetBookCommentsAsync(string bookId)
        {
            return await this.GetAsync(book => book.BookId == bookId)
                            .ContinueWith(
                                         b => b.Result.Comments
                                        );
        }

        public async Task<IEnumerable<Tag>> GetBookTagsAsync(string bookId)
        {
            return await this.GetAsync(book => book.BookId == bookId)
                             .ContinueWith(
                                         b => b.Result.BookTags
                                                      .Select(bt => bt.Tag)
                                        );
        }


        public async Task RemoveBookAync(string bookId)
        {
            var bookToRemove = this.GetByIdAsync(bookId).GetAwaiter().GetResult();
            await this.DeleteAsync(bookToRemove);
        }
    }
}
