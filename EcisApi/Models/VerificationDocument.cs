using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class VerificationDocument : BaseModel
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public string DocumentName { get; set; }
        public string ResourceType { get; set; }
        public string ResourceUrl { get; set; }
        public string UploaderType { get; set; }
        
        public int VerificationProcessID { get; set; }
        public int DocumentTypeID { get; set; }

        public VerificationProcess VerificationProcess { get; set; }
        public DocumentType DocumentType { get; set; }
    }
}
