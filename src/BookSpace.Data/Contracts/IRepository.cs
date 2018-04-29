using BookSpace.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookSpace.Data.Contracts
{
    public interface IRepository<TEntity> 
                     where TEntity : class
    {
        Task<TEntity> GetByIdAsync(object id);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<IQueryable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);

        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);

        PagedResult<TEntity> GetPaged(IQueryable<TEntity> query, int page, int pageSize);
    }
}
