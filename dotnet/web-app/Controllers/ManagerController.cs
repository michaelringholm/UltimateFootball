using System;
using System.Threading.Tasks;
using dk.opusmagus.fd.bl;
using dk.opusmagus.fd.dtl;
using Microsoft.AspNetCore.Mvc;

namespace dk.opusmagus.fd.web.controllers {
    
    [ApiController]
    [Route("api/manager")]
    public class ManagerController : ControllerBase {
        public ShowManagerDetailsCommand ShowManagerDetailsCommand { get; }

        public ManagerController(ShowManagerDetailsCommand showManagerDetailsCommand) {
            this.ShowManagerDetailsCommand = showManagerDetailsCommand;
        }

        [HttpPost("show-details")]
        public async Task<ShowManagerDetailsCommandOutputModel> ShowDetails(ManagerDTO manager) {
            return await ShowManagerDetailsCommand.Execute(new ShowManagerDetailsCommandInputModel{ ManagerId = manager.Id });
        }
    }
}