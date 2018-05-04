using BookSpace.Repositories.Contracts;
using Microsoft.EntityFrameworkCore.Query;
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
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);

        Task<IEnumerable<TProperty>> GetOneToManyAsync<TProperty>
                                   (Expression<Func<TEntity, bool>> where,
                                    Expression<Func<TEntity, IEnumerable<TProperty>>> selector);

        Task<IEnumerable<TResultProperty>> GetManyToManyAsync<TProperty, TResultProperty>
                                    (Expression<Func<TEntity, bool>> where,
                                     Expression<Func<TEntity, IEnumerable<TProperty>>> selectorMany,
                                     Expression<Func<TProperty, TResultProperty>> selector);

        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);

        Task<PagedResult<TEntity>> GetPaged(int page, int pageSize);
        Task<IEnumerable<TEntity>> FindByExpressionOrdered<TProperty>
                                    (Expression<Func<TEntity, TProperty>> order,
                                     int recordsCount);

        Task<int> GetCount();
    }
}
