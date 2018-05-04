using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face.Contract;

namespace BookSpace.CognitiveServices.Contract
{
    public interface IFaceService
    {
        Task<FaceAttributes[]> DetectFaceAtribytesAsync(string url);
    }
}
