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

        public void Add(TEntity entity)
        {
             this.dbContext.SetAdded(entity);
        }

        public void Update(TEntity entity)
        {
           this.dbContext.SetUpdated(entity);
        }

        public void Delete(TEntity entity)
        {
            this.dbContext.SetDeleted(entity);
        }
    }
}
