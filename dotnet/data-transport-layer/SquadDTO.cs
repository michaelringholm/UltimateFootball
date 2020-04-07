using System;

namespace dk.opusmagus.fd.dtl
{
    public class SquadDTO
    {
        public Guid GUID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string MIMEType { get; set; }
        public DateTime LastUpdated { get; set; }
        public byte[] Contents { get; set; }
    }
}
