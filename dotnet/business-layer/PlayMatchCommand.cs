using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace dk.opusmagus.fd.bl
{
    public class PlayMatchCommand : ICommand<PlayMatchCommandInputModel, Task<PlayMatchCommandOutputModel>>
    {
        public PlayMatchCommand()
        {
            //if (orderService == null) throw new Exception("orderService is null");
            //if (paymentService == null) throw new Exception("paymentService is null");
            //if (logger == null) throw new Exception("logger is null");
            //this.orderService = orderService;
            //this.paymentService = paymentService;
            //this.logger = logger;
        }

        public async Task<PlayMatchCommandOutputModel> Execute(PlayMatchCommandInputModel input)
        {
            return null;
        }
    }
}