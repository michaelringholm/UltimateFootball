using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using dk.opusmagus.fd.dtl;
using Newtonsoft.Json;

namespace dk.opusmagus.fd.dal.local
{
    public class LocalFileStoreManagerDAO : LocalFileStoreBase, IManagerDAO
    {
        public Task<ManagerDTO> Restore(Guid managerGuid)
        {
            var json = File.ReadAllText($"{ROOT_PATH}/managers/{managerGuid}.json");
            var manager = JsonConvert.DeserializeObject<ManagerDTO>(json);
            return Task.FromResult(manager);
        }

        public Task Store(ManagerDTO manager)
        {
            var json = JsonConvert.SerializeObject(manager);
            File.WriteAllText($"{ROOT_PATH}/managers/{manager.Id}.json", json);
            return Task.CompletedTask;
        }
    }
}
