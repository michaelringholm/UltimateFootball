using System.Collections.Generic;
using System.Threading.Tasks;
using dk.opusmagus.fd.dtl;

namespace dk.opusmagus.fd.dal
{
    public interface ISquadDAO
    {
        Task Store(PlayerDTO player);
        Task<List<PlayerDTO>> Restore(string teamId);
    }
}