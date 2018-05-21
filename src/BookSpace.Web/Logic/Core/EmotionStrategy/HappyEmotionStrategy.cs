using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSpace.Web.Logic.Interfaces;
using Microsoft.ProjectOxford.Face.Contract;

namespace BookSpace.Web.Logic.Core.EmotionStrategy
{
    public class HappyEmotionStrategy : IEmotionStrategy
    {
        private const float _happinesScore = 0.5f;
        private const float _sadnessScore = 0.2f;

        public string Emotion => "happy";

        public bool isApplicable(FaceAttributes faceAttributes)
        {
            if (faceAttributes.Emotion.Happiness > _happinesScore && faceAttributes.Emotion.Sadness < _sadnessScore)
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
