using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSpace.Web.Logic.Interfaces;
using Microsoft.ProjectOxford.Face.Contract;

namespace BookSpace.Web.Logic.Core.EmotionStrategy
{
    public class SadEmotionStrategy : IEmotionStrategy
    {
        private const float _happinesScore = 0.2f;
        private const float _sadnessScore = 0.4f;
        public string Emotion => "sad";

        public bool isApplicable(FaceAttributes faceAttributes)
        {
            if (faceAttributes.Emotion.Happiness < 0.2f && faceAttributes.Emotion.Sadness > 0.4f)
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
