using System.Collections.Generic;
using System.Threading.Tasks;
using dk.opusmagus.fd.dtl;

namespace dk.opusmagus.fd.dal
{
    public interface ITransferMarket
    {
        Task<List<PlayerDTO>> GetPlayers(int maxResults = 20);
    }
}