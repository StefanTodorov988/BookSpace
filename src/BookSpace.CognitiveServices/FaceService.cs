using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BookSpace.CognitiveServices.Contract;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

namespace BookSpace.CognitiveServices
{
    public class FaceService : IFaceService
    {
        private readonly IFaceServiceClient faceServiceClient;

        public FaceService(FaceServiceStorageInfo info )
        {
            faceServiceClient = new FaceServiceClient(info.SubscriptionKey, info.ApiRoot);
        }

        public async Task<FaceAttributes[]> DetectFaceAtribytesAsync(string url)
        {
            var faces = await faceServiceClient.DetectAsync(url, true,
                true,
                new FaceAttributeType[] {
                    FaceAttributeType.Gender,
                    FaceAttributeType.Age,
                    FaceAttributeType.Emotion,
                });
            var faceAtributes = faces.Select(face => face.FaceAttributes).ToArray();
            return faceAtributes;
        }
    }
}
