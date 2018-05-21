using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face.Contract;

namespace BookSpace.Web.Logic.Interfaces
{
    public interface IEmotionStrategy
    {
        string Emotion { get; }
        bool isApplicable(FaceAttributes faceAttributes);
    }
}
