using System;
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
            var pageRecords = await this.GetPaged(take,skip);

            return pageRecords.Results;
        }

        public async Task<IEnumerable<Author>> GetBookAuthorsAsync(string bookId)
        {
            var book = await this.GetAsync(b => b.BookId == bookId);

            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            return book.BookAuthors.Select(ba => ba.Author);
        }

        public async Task<IEnumerable<Author>> GetBookAuthorsAsync2(string bookId)
        {
            var book = await this.GetAsync(b => b.BookId == bookId);

            if(book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            return book.BookAuthors.Select(ba => ba.Author);
        }

        public async Task<IEnumerable<Genre>> GetBookGenresAsync(string bookId)
        {
            var book = await this.GetAsync(b => b.BookId == bookId);

            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            return book.BookGenres.Select(ba => ba.Genre);
        }

        public async Task<IEnumerable<Comment>> GetBookCommentsAsync(string bookId)
        {
            var book = await this.GetAsync(b => b.BookId == bookId);

            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            return book.Comments;
        }

        public async Task<IEnumerable<Tag>> GetBookTagsAsync(string bookId)
        {
            var book = await this.GetAsync(b => b.BookId == bookId);

            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            return book.BookTags.Select(ba => ba.Tag);
        }


        public async Task RemoveBookAync(string bookId)
        {
            var bookToRemove = await this.GetByIdAsync(bookId);
            await this.DeleteAsync(bookToRemove);
        }
    }
}
