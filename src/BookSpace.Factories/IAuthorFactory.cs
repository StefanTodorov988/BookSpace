using BookSpace.Models;

namespace BookSpace.Factories
{
    public interface IAuthorFactory
    {
        Author Create(string authorId, string name);
    }
}
