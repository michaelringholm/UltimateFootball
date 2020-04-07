using System;
using System.Collections.Generic;
using dk.opusmagus.fd.dtl;

namespace dk.opusmagus.fd.sl
{
    public interface IDocumentService
    {
        List<PlayerDTO> GetDocuments(String attrName, String attrValue);
    }
}
