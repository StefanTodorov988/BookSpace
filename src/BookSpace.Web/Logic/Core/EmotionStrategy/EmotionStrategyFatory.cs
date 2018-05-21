using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSpace.Web.Logic.Core.EmotionStrategy.Contract;
using BookSpace.Web.Logic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ProjectOxford.Face.Contract;

namespace BookSpace.Web.Logic.Core.EmotionStrategy
{
    public class EmotionStrategyFatory : IEmotionStrategyFactory
    {
        private readonly IServiceProvider _iServiceProvider;
        public EmotionStrategyFatory(IServiceProvider iServiceProvider)
        {
            this._iServiceProvider = iServiceProvider;
           
        }
        public IEmotionStrategy CreatEmotionStrategy(FaceAttributes faceAttributes)
        {
            return _iServiceProvider
                .GetServices<IEmotionStrategy>()
                .FirstOrDefault(strategy => strategy.isApplicable(faceAttributes));
        }
    }
}
