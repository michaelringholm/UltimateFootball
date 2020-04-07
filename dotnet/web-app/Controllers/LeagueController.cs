using System;
using System.Threading.Tasks;
using dk.opusmagus.fd.bl;
using Microsoft.AspNetCore.Mvc;

namespace dk.opusmagus.fd.web.controllers {
    
    [ApiController]
    [Route("api/league")]
    public class LeagueController : ControllerBase {
        public ShowLeagueTableCommand ShowLeagueTableCommand { get; }

        public LeagueController(ShowLeagueTableCommand showLeagueTableCommand) {
            this.ShowLeagueTableCommand = showLeagueTableCommand;
        }

        [HttpPost("show-league-table")]
        public async Task<ShowLeagueTableOutputModel> ShowLeagueTable(Guid leagueId) {
            return await ShowLeagueTableCommand.Execute(new ShowLeagueTableInputModel{ LeagueId = Guid.NewGuid() });
        }
    }
}