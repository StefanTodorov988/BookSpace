using BookSpace.Models;

namespace BookSpace.Factories
{
    public interface IApplicationUserFactory
    {
        ApplicationUser CreateUser(string username, string email);
    }
}
