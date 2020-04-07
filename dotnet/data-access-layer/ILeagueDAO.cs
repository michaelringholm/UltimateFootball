using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dk.opusmagus.fd.dtl;

namespace dk.opusmagus.fd.dal
{
    public interface ILeagueDAO
    {
        Task<List<TeamDTO>> RestoreTeams(Guid leagueId);
        Task<LeagueDTO> Restore(Guid leagueId);
    }
}