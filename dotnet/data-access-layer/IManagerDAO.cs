using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dk.opusmagus.fd.dtl;

namespace dk.opusmagus.fd.dal
{
    public interface IManagerDAO
    {
        Task<ManagerDTO> Restore(Guid managerId);
    }
}