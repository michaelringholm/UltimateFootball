using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dk.opusmagus.fd.dal;
using dk.opusmagus.fd.dtl;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace dk.fp.pinfo.dal.cosmos
{
    public class CosmosLeagueDAO : ILeagueDAO
    {        
        private readonly ILogger logger;
        public CosmosDBFacade<TeamDTO> CosmosDBFacade{get; private set;}
        private readonly string endpoint;
        private readonly string key;
        private readonly string databaseId;
        private readonly string containerId;

        public CosmosLeagueDAO(ILogger logger, string endpoint, string key, string databaseId, string containerId) {
            if(logger == null) throw new Exception("Constructor parameter logger was null!");
            if(String.IsNullOrEmpty(endpoint)) throw new Exception("Constructor parameter endpoint was null!");
            if(String.IsNullOrEmpty(key)) throw new Exception("Constructor parameter key was null!");
            if(String.IsNullOrEmpty(databaseId)) throw new Exception("Constructor parameter databaseId was null!");
            if(String.IsNullOrEmpty(containerId)) throw new Exception("Constructor parameter containerId was null!");
            this.logger = logger;
            this.endpoint = endpoint;
            this.key = key;
            this.databaseId = databaseId;
            this.containerId = containerId;
            CosmosDBFacade = new CosmosDBFacade<TeamDTO>(endpoint, key, databaseId, containerId, pp => pp.Id, 400, "/partitionKey");
        }

        public async Task<List<TeamDTO>> RestoreTeams(Guid leagueId)
        {
            logger.LogDebug("CosmosPensionPlanDAO.Restore called!");
            var teams = (await CosmosDBFacade.GetItems("select * from c.Data", "partitionKey", 20));
            return teams;
        }

        public async Task<LeagueDTO> Restore(Guid leagueId)
        {
            throw new NotImplementedException();
        }
    }
}
