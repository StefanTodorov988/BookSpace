using BookSpace.Models;

namespace BookSpace.Factories
{
    public interface ICommentFactory
    {
        Comment Create(string id, string content);
    }
}
