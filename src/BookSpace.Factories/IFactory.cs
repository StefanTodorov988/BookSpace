using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookSpace.Factories
{
    public interface IFactory<TEntity, TModel>
    {
        TEntity Create(TModel model);
    }
}
