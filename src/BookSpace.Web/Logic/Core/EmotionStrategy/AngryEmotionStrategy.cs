using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSpace.Web.Logic.Interfaces;
using Microsoft.ProjectOxford.Face.Contract;

namespace BookSpace.Web.Logic.Core.EmotionStrategy
{
    public class AngryEmotionStrategy : IEmotionStrategy
    {
        private const float _angerScore = 0.34f;
        public string Emotion => "angry";

        public bool isApplicable(FaceAttributes faceAttributes)
        {
            if (faceAttributes.Emotion.Anger > _angerScore)
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
