using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookSpace.Data;
using BookSpace.Data.Contracts;
using BookSpace.Models;
using BookSpace.Repositories.Contracts;

namespace BookSpace.Repositories
{
    public class BookRepository :  BaseRepository<BookDBModel>, IBookRepository
    {
        public BookRepository(IDbContext dbContext) : base(dbContext) {}


        public BookDBModel GetBookByNameAsync(string name)
        {
          // return this.
        }

        public IEnumerable<BookDBModel> GetPageOfBooksAsync(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public AuthorDBModel GetBookAuthorAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public GenreDBModel GetBookGenreAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CommentDBModel> GetBookCommentsAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TagDBModel> GetBookTagsAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task CreateBookAsync(BookDBModel book)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBookAsync(BookDBModel book)
        {
            throw new NotImplementedException();
        }

        public Task RemoveBookAync(int bookId)
        {
            throw new NotImplementedException();
        }
    }
}
