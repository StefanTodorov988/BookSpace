using System;
using System.Collections.Generic;
using System.Text;
using BookSpace.Data.Contracts;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BookSpace.Data
{
    public class BaseRepository<TEntity, TResult> : IRepository<TEntity, TResult> 
                                                    where TEntity : class
    {
        private readonly IDbContext dbContext;

        public BaseRepository(IDbContext dbCtx)
        {
            this.dbContext = dbCtx ?? throw new ArgumentNullException(nameof(dbCtx));
        }
    }
}
