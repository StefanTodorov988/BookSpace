using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSpace.Data;
using BookSpace.Data.Contracts;
using BookSpace.Models;
using BookSpace.Models.Enums;
using BookSpace.Repositories.Contracts;

namespace BookSpace.Repositories
{
    public class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
       
        public ApplicationUserRepository(IDbContext dbContext) : base(dbContext) {}

        public async Task<ApplicationUser> GetUserByUsernameAsync(string username)
        {
            return await this.GetAsync(u => u.UserName == username );
        }

        public async Task<IEnumerable<ApplicationUser>> GetPageOfUsersAscync(int take, int skip)
        {
            var users = await this.GetAllAsync();

            return this.GetPaged(users, take, skip).Results;
        }


        public async Task<IEnumerable<Book>> GetUserBooksAsync(string userId, BookState state)
        {
            return await this.GetAsync(user => user.Id == userId)
                             .ContinueWith(
                                           u => u.Result.BookUsers
                                                        .Where( bu =>bu.State == state)
                                                        .Select(b => b.Book)
                                          );

        }
    }
}
