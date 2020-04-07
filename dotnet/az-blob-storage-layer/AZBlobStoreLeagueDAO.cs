using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dk.opusmagus.fd.dal;
using dk.opusmagus.fd.dtl;
using Microsoft.Extensions.Logging;

namespace dk.opusmagus.fd.dal.blob
{
    public class AZBlobStoreLeagueDAO : ILeagueDAO
    {
        private readonly ILogger logger;
        private readonly AzureBlobService azureBlobService;
        private readonly string blobName;
        private readonly Object lockObj = "";

        public AZBlobStoreLeagueDAO(ILogger logger, string blobContainerConnString, string blobName) {
            if(logger == null) throw new Exception("parameter logger was null!");
            if(blobContainerConnString == null) throw new Exception("parameter blobContainerConnString was null!");
            if(blobName == null) throw new Exception("parameter blobName was null!");
            this.logger = logger;
            azureBlobService = new AzureBlobService(blobContainerConnString);
            this.blobName = blobName;
        }

        public async Task<LeagueDTO> Restore(Guid leagueId)
        {
            //return (await GetLeague()).Where(c => c.IMONumber.Equals(imoNumber)).FirstOrDefault();
            return null;
        }


        public async Task<List<TeamDTO>> RestoreTeams(Guid leagueId)
        {
            logger.LogDebug("AZBlobStoreLeagueDAO.GetLeague called!");
            var teamsData = (await azureBlobService.getBlobContents<string>("leagues", "national-league.json"));
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
