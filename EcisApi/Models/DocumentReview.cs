using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class DocumentReview : BaseModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsAccepted { get; set; }

        public int? VerificationDocumentId { get; set; }

        public virtual VerificationDocument VerificationDocument { get; set; }
    }
}
