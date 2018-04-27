using BookSpace.Models;

namespace BookSpace.Factories
{
    public interface IGenreFactory
    {
        Genre Create(string genreId, string name);
    }
}
