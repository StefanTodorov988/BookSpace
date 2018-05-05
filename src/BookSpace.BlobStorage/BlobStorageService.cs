using BookSpace.BlobStorage.Contracts;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace BookSpace.BlobStorage
{
    public class BlobStorageService : IBlobStorageService
    {
        public readonly CloudBlobClient client;
        public readonly SharedAccessBlobPolicy policy;
        public BlobStorageService(BlobStorageInfo info)
        {
            var account = new CloudStorageAccount(new StorageCredentials(info.AccountName, info.Key), true);
            this.client = account.CreateCloudBlobClient();
            this.policy = new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.Now.AddDays(100)
            };
        }
        public async Task<BlobObjectInfo> GetAsync(string name, string container)
        {
            var containerInstance = this.client.GetContainerReference(container);
            if (!await containerInstance.ExistsAsync())
            {
                throw new ArgumentNullException();
            }
            var blob = await containerInstance.GetBlobReferenceFromServerAsync(name);
            var sas = blob.GetSharedAccessSignature(policy);
            return new BlobObjectInfo
            {
                Url = blob.Uri + sas
            };
        }
        public async Task UploadAsync(string name, string container, byte[] data)
        {
            var containerInstance = this.client.GetContainerReference(container);
            if (await containerInstance.CreateIfNotExistsAsync())
            {
                await containerInstance.SetPermissionsAsync(
                     new BlobContainerPermissions()
                     {
                         PublicAccess = BlobContainerPublicAccessType.Off
                     });

            }
            var blob = containerInstance.GetBlockBlobReference(name);
                await blob.UploadFromByteArrayAsync(data, 0 ,data.Length);
        }
    }
}
