using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSpace.Web.Logic.Interfaces;
using Microsoft.ProjectOxford.Face.Contract;

namespace BookSpace.Web.Logic.Core.EmotionStrategy
{
    public class SuprisedEmotionStrategy : IEmotionStrategy
    {
        private const float _supriseScore = 0.40f;
        public string Emotion => "suprised";
        public bool isApplicable(FaceAttributes faceAttributes)
        {
            if (faceAttributes.Emotion.Surprise > _supriseScore)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
