using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class VerificationConfirmDocument : BaseModel
    {
        public int Id { get; set; }
        public string DocumentType { get; set; }
        public string DocumentUrl { get; set; }
        public string DocumentName { get; set; }
        public long DocumentSize { get; set; }

        public int? VerificationConfirmRequirementId { get; set; }

        public virtual VerificationConfirmRequirement VerificationConfirmRequirement { get; set; }
    }
}
