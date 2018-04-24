using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BookSpace.Data.Contracts
{
    public interface IDbContext
    {
        DbSet<TEntity> DbSet<TEntity>() where TEntity : class;
    }
}
