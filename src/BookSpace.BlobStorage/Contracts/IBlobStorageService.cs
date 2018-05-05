using System.IO;
using System.Threading.Tasks;

namespace BookSpace.BlobStorage.Contracts
{
    public interface IBlobStorageService
    {
        Task UploadAsync(string name, string container, byte[] data);
        Task<BlobObjectInfo> GetAsync(string name, string container);
    }
}
