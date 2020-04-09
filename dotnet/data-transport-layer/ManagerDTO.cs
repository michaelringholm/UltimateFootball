using System;

namespace dk.opusmagus.fd.dtl
{
    public class ManagerDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TeamId { get; set; }
        public int Season { get; set; }
        public int Round { get; set; }
    }
}
