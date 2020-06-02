﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dk.opusmagus.fd.dtl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace dk.opusmagus.fd.dal.blob
{
    public class AZBlobStoreLeagueDAO : ILeagueDAO
    {
        private readonly ILogger logger;
        private readonly AzureBlobService azureBlobService;

        public AZBlobStoreLeagueDAO(ILogger<AZBlobStoreLeagueDAO> logger, IConfiguration configuration) {
            if(logger == null) throw new Exception("parameter logger was null!");
            if(configuration == null) throw new Exception("parameter configuration was null!");
            var accountName = configuration.GetValue<string>("accountName");
            var containerName = configuration.GetValue<string>("containerName");
            if(accountName == null) throw new Exception("parameter accountName was null!");
            if(containerName == null) throw new Exception("parameter containerName was null!");
            this.logger = logger;
            azureBlobService = new AzureBlobService(accountName, containerName);
        }

        public Task<LeagueDTO> Restore(Guid leagueId)
        {
            //return (await GetLeague()).Where(c => c.IMONumber.Equals(imoNumber)).FirstOrDefault();
            //return null;
            return Task.FromResult(new LeagueDTO { Name = "National League", Id = leagueId });
        }


        public async Task<List<TeamDTO>> RestoreTeams(Guid leagueId)
        {
            logger.LogDebug("AZBlobStoreLeagueDAO.GetLeague called!");
            var teamsData = (await azureBlobService.getBlobContents<string>("data", "national-league.json"));
            var teams = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TeamDTO>>(teamsData);
            return teams;
        }       


        /*public async Task Store(CertificateDTO certificate)
        {
            var certificates = (await GetCertificates());
            var existingCertificate = certificates.Where(c => c.IMONumber.Equals(certificate.IMONumber)).FirstOrDefault();
            if(existingCertificate != null) {
                // Update
                existingCertificate.Issued = certificate.Issued;
            }
            else {
                // Insert
                certificates.Add(certificate);
            }
            var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(certificates);
            await azureBlobService.putBlobContents("certificates", blobName, jsonData);
        }*/
    }
}
