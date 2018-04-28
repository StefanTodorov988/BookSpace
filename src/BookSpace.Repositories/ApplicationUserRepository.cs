using System;
using System.Collections.Generic;
using System.Linq;
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
     
        public ApplicationUser GetUserByUsernameAsync(string username)
        {
            return this.GetAsync(u => u.UserName == username ).GetAwaiter().GetResult();
        }

        public IEnumerable<ApplicationUser> GetPageOfUsersAscync(int take, int skip)
        {
            return this.GetAllAsync().GetAwaiter().GetResult().Skip(skip).Take(take);
        }

        public IEnumerable<Book> GetUserReadBooksAsync(string userId)
        {
            return this.GetByIdAsync(userId).GetAwaiter().GetResult()
                                                         .BookUsers
                                                         .Where(bu => bu.State == BookState.Read)
                                                         .Select(bu => bu.Book);

        }

        public IEnumerable<Book> GetUserBooksToReadAsync(string userId)
        {
            return this.GetByIdAsync(userId).GetAwaiter().GetResult()
                                                         .BookUsers
                                                         .Where(bu => bu.State == BookState.ToRead)
                                                         .Select(bu => bu.Book);
        }

        public IEnumerable<Book> GetUserFavouriteBooksAsync(string userId)
        {
            return this.GetByIdAsync(userId).GetAwaiter().GetResult()
                                                         .BookUsers
                                                         .Where(bu => bu.State == BookState.Favourite)
                                                         .Select(bu => bu.Book);
        }
    }
}
