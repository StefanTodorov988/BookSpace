using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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


        public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            return await this.dbContext.DbSet<TEntity>().Where(where).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            return await this.dbContext.DbSet<TEntity>().Where(where).FirstOrDefaultAsync<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
               dbContext.SetAdded(entity);
            });
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                dbContext.SetUpdated(entity);
            });
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                dbContext.SetDeleted(entity);
            });
        }
    }
}
