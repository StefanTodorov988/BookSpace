using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookSpace.Data.Contracts
{
    public interface IRepository<TEntity> 
                     where TEntity : class
    {
        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
