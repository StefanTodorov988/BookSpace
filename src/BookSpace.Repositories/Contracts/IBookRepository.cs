using System.Collections.Generic;
using System.Threading.Tasks;
using BookSpace.Data.Contracts;
using BookSpace.Models;

namespace BookSpace.Repositories.Contracts
{
    public interface IBookRepository : IRepository<Book>
    {
        Book GetBookByTitleAsync(string title);
        IEnumerable<Book> GetPageOfBooksAsync(int skip, int take);
        IEnumerable<Author> GetBookAuthorsAsync(string bookId);
        IEnumerable<Genre> GetBookGenresAsync(string bookId);
        IEnumerable<Comment> GetBookCommentsAsync(string bookId);
        IEnumerable<Tag> GetBookTagsAsync(string bookId);

        void CreateBookAsync(Book book);
        void UpdateBookAsync(Book book);
        void RemoveBookAync(string bookId);
    }
}
