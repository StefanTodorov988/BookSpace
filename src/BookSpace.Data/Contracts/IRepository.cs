using System;
using System.Collections.Generic;
using System.Text;

namespace BookSpace.Data.Contracts
{
    public interface IRepository<in TEntity, out TResult> 
                     where TEntity : class 
    {

    }
}
