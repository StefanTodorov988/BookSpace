using BookSpace.Web.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace BookSpace.Web.Logic.Core.Strategy
{
    public class SearchStrategyFactory : ISearchStrategyFactory
    {
        private readonly IUnityContainer _unityContainer;

        public SearchStrategyFactory(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public ISearchStrategy GetSearchStrategy(string radioFilter)
        {
            if (_unityContainer.IsRegistered<ISearchStrategy>(radioFilter))
            {
                return _unityContainer.Resolve<ISearchStrategy>(radioFilter);
            }

            return null;
        }
    }
}
