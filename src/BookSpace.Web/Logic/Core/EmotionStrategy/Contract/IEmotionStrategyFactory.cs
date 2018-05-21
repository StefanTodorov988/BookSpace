using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSpace.Web.Logic.Interfaces;
using Microsoft.ProjectOxford.Face.Contract;

namespace BookSpace.Web.Logic.Core.EmotionStrategy.Contract
{
    public interface IEmotionStrategyFactory
    {
        IEmotionStrategy CreatEmotionStrategy(FaceAttributes faceAttributes);
    }
}
