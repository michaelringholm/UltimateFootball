using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using dk.opusmagus.fd.dal.blob;
using dk.opusmagus.fd.dtl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace dk.opusmagus.fd.dal.local
{
    public class AZBlobStoreManagerDAO : IManagerDAO
    {
        private readonly ILogger logger;
        private readonly AzureBlobService azureBlobService;
        private readonly string blobContainerName;

        public AZBlobStoreManagerDAO(ILogger<AZBlobStoreManagerDAO> logger, IConfiguration configuration) {
            if(logger == null) throw new Exception("parameter logger was null!");
            if(configuration == null) throw new Exception("parameter configuration was null!");
            this.logger = logger;
            var accountName = configuration.GetValue<string>("accountName");
            var containerName = configuration.GetValue<string>("containerName");
            if(accountName == null) throw new Exception("parameter accountName was null!");
            if(containerName == null) throw new Exception("parameter containerName was null!");
            azureBlobService = new AzureBlobService(accountName, containerName);
            this.blobContainerName = configuration.GetValue<string>("blobContainerName");
        }

        public async Task<ManagerDTO> Restore(Guid managerGuid)
        {
            var json = (await azureBlobService.getBlobContents<string>(blobContainerName, $"managers/{managerGuid}.json"));
            var manager = JsonConvert.DeserializeObject<ManagerDTO>(json);
            return manager;
            //return Task.FromResult(manager);
        }

        public async Task Store(ManagerDTO manager)
        {
            var json = JsonConvert.SerializeObject(manager);
            await azureBlobService.putBlobContents($"managers/{manager.Id}.json", json);
        }
    }
}
