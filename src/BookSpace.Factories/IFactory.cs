using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookSpace.Factories
{
    public interface IFactory<TEntity, TModel> where TEntity: class, where TModel : class
    {
        TEntity Create(TModel model);
    }
}
