using System.Collections.Generic;
using System.Threading.Tasks;
using BookSpace.Data.Contracts;
using BookSpace.Models;
using BookSpace.Models.Enums;

namespace BookSpace.Repositories.Contracts
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetUserByUsernameAsync(string username);
        Task<IEnumerable<ApplicationUser>> GetPageOfUsersAscync(int take, int skip);
        Task<IEnumerable<Book>> GetUserBooksAsync(string userId, BookState state);

        // TODO : ADD MORE METHODS WHEN NEEDED
    }
}
