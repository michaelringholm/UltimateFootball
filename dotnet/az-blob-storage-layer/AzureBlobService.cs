using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace dk.opusmagus.fd.dal.blob 
{
    public class AzureBlobService {
        private readonly CloudStorageAccount storageAccount;
        private readonly CloudBlobClient blobClient;

        public AzureBlobService(string blobContainerConnString) {
            storageAccount = CloudStorageAccount.Parse(blobContainerConnString);
            blobClient = storageAccount.CreateCloudBlobClient();
        }

        public async Task<IEnumerable<IListBlobItem>> getBlobItems(string blobContainerName, int maxResults)
        {            
            var container = blobClient.GetContainerReference(blobContainerName);
            var blobItems = (await container.ListBlobsSegmentedAsync("", true, BlobListingDetails.All, maxResults, null, null, null)).Results;
            return blobItems;
        }        

        public async Task<byte[]> getBlobContents(string blobContainerName, string blobName) {
            var container = blobClient.GetContainerReference(blobContainerName);
            var blobBlockRef = container.GetBlockBlobReference(blobName);
            await blobBlockRef.FetchAttributesAsync();
            byte[] buffer = new byte[blobBlockRef.Properties.Length];       
            await blobBlockRef.DownloadToByteArrayAsync(buffer, 0);
            return buffer;
        }

        public async Task<string> getBlobContents<String>(string blobContainerName, string blobName) {
            return Encoding.UTF8.GetString((await getBlobContents(blobContainerName, blobName)));
        }

        internal async Task putBlobContents(string blobContainerName, string blobName, string jsonData)
        {
            var container = blobClient.GetContainerReference(blobContainerName);
            var blobBlockRef = container.GetBlockBlobReference(blobName);
            await blobBlockRef.FetchAttributesAsync();
            await blobBlockRef.UploadTextAsync(jsonData);
        }
    }
}