using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookSpace.Data.Contracts;
using Microsoft.EntityFrameworkCore;


namespace BookSpace.Data
{
    public class BaseRepository<TEntity> : IRepository<TEntity> 
                                                    where TEntity : class
    {
        private readonly IDbContext dbContext;

        public BaseRepository(IDbContext dbCtx)
        {
            this.dbContext = dbCtx ?? throw new ArgumentNullException(nameof(dbCtx));
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await this.dbContext.DbSet<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.dbContext.DbSet<TEntity>().ToListAsync();
        }

        public async Task Add(TEntity entity)
        {
            await this.dbContext.DbSet<TEntity>().AddAsync(entity);
        }

        public Task Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
