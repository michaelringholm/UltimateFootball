using System;
using System.Threading.Tasks;
using dk.opusmagus.fd.bl;
using dk.opusmagus.fd.dtl;
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
        public async Task<ShowLeagueTableOutputModel> ShowLeagueTable(LeagueDTO league) {
            return await ShowLeagueTableCommand.Execute(new ShowLeagueTableInputModel{ LeagueId = league.Id });
        }
    }
}