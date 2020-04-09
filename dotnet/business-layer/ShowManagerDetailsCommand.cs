using System;
using System.Threading.Tasks;
using dk.opusmagus.fd.dal;
using Microsoft.Extensions.Logging;

namespace dk.opusmagus.fd.bl
{
    public class ShowManagerDetailsCommand : ICommand<ShowManagerDetailsCommandInputModel, Task<ShowManagerDetailsCommandOutputModel>>
    {
        public IManagerDAO ManagerDAO { get; }
        public ShowManagerDetailsCommand(IManagerDAO managerDAO)
        {
            if (managerDAO == null) throw new Exception("managerDAO is null");
            this.ManagerDAO = managerDAO;
        }        

        public async Task<ShowManagerDetailsCommandOutputModel> Execute(ShowManagerDetailsCommandInputModel input)
        {
            var manager = await ManagerDAO.Restore(input.ManagerId);
            return new ShowManagerDetailsCommandOutputModel { Manager = manager };
        }
    }
}