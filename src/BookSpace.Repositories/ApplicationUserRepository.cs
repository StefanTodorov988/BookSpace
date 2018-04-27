using System;
using System.Collections.Generic;
using System.Linq;
using BookSpace.Data;
using BookSpace.Data.Contracts;
using BookSpace.Models;
using BookSpace.Repositories.Contracts;

namespace BookSpace.Repositories
{
    public class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {

        ApplicationUserRepository(IDbContext dbContext) : base(dbContext) {}
     
        public ApplicationUser GetUserByUsernameAsync(string username)
        {
            return this.GetAsync(u => u.UserName == username ).GetAwaiter().GetResult();
        }

        public IEnumerable<ApplicationUser> GetPageOfUsersAscync(int take, int skip)
        {
            return this.GetAllAsync().GetAwaiter().GetResult().Skip(skip).Take(take);
        }

        public IEnumerable<BookDBModel> GetUserReadBooksAsync(string userId)
        {
            return this.GetByIdAsync(userId).GetAwaiter().GetResult().BookUsers.Select(b => b.Book);
        }

        // TODO: MAKE THE BUSINESS LOGIC CLEAR

        public IEnumerable<BookDBModel> GetUserBooksToReadAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
