using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookSpace.Data.Contracts
{
    public interface IDbContext
    {
        DbSet<TEntity> DbSet<TEntity>() where TEntity : class;

        Task<int> SaveAsync();
    }
}
