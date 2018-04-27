using BookSpace.Models;

namespace BookSpace.Factories
{
    public interface ITagFactory
    {
        Tag Create(string tagId, string value);
    }
}
