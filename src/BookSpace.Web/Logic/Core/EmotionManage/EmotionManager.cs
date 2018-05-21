using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSpace.CognitiveServices.Contract;
using BookSpace.Web.Logic.Core.EmotionManage.Contract;
using BookSpace.Web.Logic.Core.EmotionStrategy.Contract;
using BookSpace.Web.Logic.Interfaces;
using BookSpace.Web.Services.SmtpService.Contract;
using Microsoft.ProjectOxford.Face.Contract;

namespace BookSpace.Web.Logic.Core.EmotionManage
{
    public class EmotionManager : IEmotionManager
    {
        private readonly IFaceService faceService;
        private readonly ISmtpSender smtpSender;
        private IEmotionStrategy emotionStrategy;
        private IEmotionStrategyFactory emotionStrategyFactory;

        private const string newProfilePictureSubject = "New profile picture";

        public EmotionManager(IFaceService faceService, ISmtpSender smtpSender, IEmotionStrategyFactory emotionStrategyFactory)
        {
            this.faceService = faceService;
            this.smtpSender = smtpSender;
            this.emotionStrategyFactory = emotionStrategyFactory;
        }
        public void ProcessSendingMessage(FaceAttributes[] faceAttributeses, string userMail,string userName)
        {
            if (FaceValidating(faceAttributeses))
            {
                this.emotionStrategy = emotionStrategyFactory.CreatEmotionStrategy(faceAttributeses[0]);
                smtpSender.SendMail(userMail, newProfilePictureSubject,GenerateMessage(emotionStrategy,userName));
            }
        }
        public string GenerateMessage(IEmotionStrategy emotionStrategy, string userName)
        {
            return $"Greetings {userName}, \n" + Environment.NewLine + Environment.NewLine +
                   "We see that you have uploaded a profile picture!\n" +
                   $"Our face recognition service recognized you as being {emotionStrategy.Emotion}. \n" + Environment.NewLine + Environment.NewLine +
                   "Best regards, \n" +
                   "Bookster Team";
        }

        public bool FaceValidating(FaceAttributes[] faceAttributeses)
        {
            if (faceAttributeses.Length > 0)
            {
                return true;
            }
            return false;
        }
    }
}
