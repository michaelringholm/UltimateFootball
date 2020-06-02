using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace dk.opusmagus.fd.dal.blob 
{
    public class AzureBlobService {
        private readonly BlobContainerClient containerClient;

        public AzureBlobService(string accountName, string containerName) {
            //storageAccount = CloudStorageAccount.Parse(blobContainerConnString);
            //blobClient = storageAccount.CreateCloudBlobClient();
            var containerEndpoint = string.Format($"https://{accountName}.blob.core.windows.net/{containerName}");
            containerClient = new BlobContainerClient(new Uri(containerEndpoint), new DefaultAzureCredential());
        }

        public async Task<List<BlobItem>> getBlobItems(int maxResults)
        {            
            var blobItems = new List<BlobItem>();
            var pages = containerClient.GetBlobsAsync().AsPages().GetAsyncEnumerator();
            while(await pages.MoveNextAsync()) {
                foreach(var blobItem in pages.Current.Values) {
                    blobItems.Add(blobItem);
                }
            }
            return blobItems;
        }        

        public async Task<byte[]> getBlobContents(string blobContainerName, string blobName) {
            var blobClient = containerClient.GetBlobClient(blobName);
            var memStream = new MemoryStream();
            await blobClient.DownloadToAsync(memStream);
            var bytes = memStream.GetBuffer();
            return bytes;
        }

        public async Task<string> getBlobContents<String>(string blobContainerName, string blobName) {
            return Encoding.UTF8.GetString((await getBlobContents(blobContainerName, blobName)));
        }

        internal async Task putBlobContents(string blobName, string jsonData)
        {
            var memStream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(jsonData));
            await containerClient.UploadBlobAsync(blobName, memStream);
        }
    }
}