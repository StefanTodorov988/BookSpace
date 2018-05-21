using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSpace.Web.Logic.Interfaces;
using Microsoft.ProjectOxford.Face.Contract;

namespace BookSpace.Web.Logic.Core.EmotionManage.Contract
{
    public interface IEmotionManager
    {
        void ProcessSendingMessage(FaceAttributes[] faceAttributeses, string userMail, string userName);
        string GenerateMessage(IEmotionStrategy emotionStrategy, string userName);
        bool FaceValidating(FaceAttributes[] faceAttributeses);
    }
}
