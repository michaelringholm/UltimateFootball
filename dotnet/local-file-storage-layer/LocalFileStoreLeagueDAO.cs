using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using dk.opusmagus.fd.dtl;
using Newtonsoft.Json;

namespace dk.opusmagus.fd.dal.local
{
    public class LocalFileStoreLeagueDAO : ILeagueDAO
    {
        public Task<LeagueDTO> Restore(Guid leagueId)
        {
            return Task.FromResult(new LeagueDTO { Name = "National League", Id = leagueId });
        }

        public Task<List<TeamDTO>> RestoreTeams(Guid leagueId)
        {
            var nationalLeagueJson = File.ReadAllText("../local-file-storage-layer/data/national-league.json");
            var nationalLeague = JsonConvert.DeserializeObject<List<TeamDTO>>(nationalLeagueJson);
            return Task.FromResult(nationalLeague);
        }
    }
}
