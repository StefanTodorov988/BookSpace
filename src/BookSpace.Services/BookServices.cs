using BookSpace.Data.Contracts;
using System;

namespace BookSpace.Services
{
    public class BookServices
    {
        private readonly IDbContext dbContext;

        public BookServices(IDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void AddTagsToBook()
        {

        }
    }
}
