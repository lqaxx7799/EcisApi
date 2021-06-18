using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class DocumentType : BaseModel
    {
        public int ID { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        public ICollection<VerificationDocument> VerificationDocuments { get; set; }
    }
}
