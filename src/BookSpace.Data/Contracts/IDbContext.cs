using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookSpace.Data.Contracts
{
    public interface IDbContext
    {
        DbSet<TEntity> DbSet<TEntity>() where TEntity : class;

        void SetAdded<TEntry>(TEntry entity) where TEntry : class;

        void SetDeleted<TEntry>(TEntry entity) where TEntry : class;

        void SetUpdated<TEntry>(TEntry entity) where TEntry : class;
    }
}
