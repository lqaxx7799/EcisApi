using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class DocumentReview : BaseModel
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public bool IsAccepted { get; set; }

        public int VerificationDocumentID { get; set; }

        public VerificationDocument VerificationDocument { get; set; }
    }
}
