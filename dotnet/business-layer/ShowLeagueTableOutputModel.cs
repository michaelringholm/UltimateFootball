using System.Collections.Generic;
using dk.opusmagus.fd.dtl;

namespace dk.opusmagus.fd.bl
{
    public class ShowLeagueTableOutputModel
    {
        public LeagueDTO League { get; internal set; }
        public List<TeamDTO> Teams { get; internal set; }
    }
}