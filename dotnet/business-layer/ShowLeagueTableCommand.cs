using System;
using System.Threading.Tasks;
using dk.opusmagus.fd.dal;
using Microsoft.Extensions.Logging;

namespace dk.opusmagus.fd.bl
{
    public class ShowLeagueTableCommand : ICommand<ShowLeagueTableInputModel, Task<ShowLeagueTableOutputModel>>
    {
        public ILeagueDAO LeagueDAO { get; }
        public ShowLeagueTableCommand(ILeagueDAO leagueDAO)
        {
            if (leagueDAO == null) throw new Exception("leagueDAO is null");
            //if (paymentService == null) throw new Exception("paymentService is null");
            //if (logger == null) throw new Exception("logger is null");
            this.LeagueDAO = leagueDAO;
            //this.paymentService = paymentService;
            //this.logger = logger;
        }        

        public async Task<ShowLeagueTableOutputModel> Execute(ShowLeagueTableInputModel input)
        {
            var league = await LeagueDAO.Restore(input.LeagueId);
            var teams = await LeagueDAO.RestoreTeams(input.LeagueId);
            return new ShowLeagueTableOutputModel { League = league, Teams = teams };
        }
    }
}