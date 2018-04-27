using BookSpace.Models;

namespace BookSpace.Factories
{
    public interface IBookFactory
    {
        Book Create(string bookId, string isbn, string title);
    }
}
