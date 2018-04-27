using System.Collections.Generic;
using System.Threading.Tasks;
using BookSpace.Data.Contracts;
using BookSpace.Models;

namespace BookSpace.Repositories.Contracts
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser GetUserByUsernameAsync(string username);
        IEnumerable<ApplicationUser> GetPageOfUsersAscync(int take, int skip);
        IEnumerable<Book> GetUserReadBooksAsync(string userId);
        IEnumerable<Book> GetUserBooksToReadAsync(string userId);
        
        // TODO : ADD MORE METHODS WHEN NEEDED
    }
}
