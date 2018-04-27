using System.Collections.Generic;
using System.Linq;
using BookSpace.Data;
using BookSpace.Data.Contracts;
using BookSpace.Models;
using BookSpace.Repositories.Contracts;

namespace BookSpace.Repositories
{
    public class BookRepository :  BaseRepository<BookDBModel>, IBookRepository
    {
        public BookRepository(IDbContext dbContext) : base(dbContext) {}


        public BookDBModel GetBookByTitleAsync(string title)
        {
            return this.GetAsync(b => b.Title == title).GetAwaiter().GetResult();
        }

        public IEnumerable<BookDBModel> GetPageOfBooksAsync(int skip, int take)
        {
            return this.GetAllAsync().GetAwaiter().GetResult()
                                                  .Skip(skip)
                                                  .Take(take);
        }

        public IEnumerable<AuthorDBModel> GetBookAuthorsAsync(string bookId)
        {
            return this.GetByIdAsync(bookId).GetAwaiter().GetResult()
                                            .BookAuthors
                                            .Select(ba => ba.Author)
                                            .ToList();
        }

        public IEnumerable<GenreDBModel> GetBookGenresAsync(string bookId)
        {
            return this.GetByIdAsync(bookId).GetAwaiter().GetResult()
                                                         .BookGenres
                                                         .Select(bg => bg.Genre)
                                                         .ToList();
        }

        public IEnumerable<CommentDBModel> GetBookCommentsAsync(string bookId)
        {
            return this.GetByIdAsync(bookId).GetAwaiter().GetResult()
                                                         .Comments
                                                         .ToList();
        }

        public IEnumerable<TagDBModel> GetBookTagsAsync(string bookId)
        {
            return this.GetByIdAsync(bookId).GetAwaiter().GetResult()
                                                         .BookTags
                                                         .Select(bt => bt.Tag)
                                                         .ToList();
        }

        public async void CreateBookAsync(BookDBModel book)
        {
            await this.AddAsync(book);

        }

        public async void UpdateBookAsync(BookDBModel book)
        {
            await this.UpdateAsync(book);
        }

        public async void RemoveBookAync(string bookId)
        {
            var bookToRemove = this.GetByIdAsync(bookId).GetAwaiter().GetResult();
            await this.DeleteAsync(bookToRemove);
        }
    }
}
