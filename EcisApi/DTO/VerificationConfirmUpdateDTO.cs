using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class VerificationConfirmUpdateDTO
    {
        public int VerificationConfirmRequirementId { get; set; }
        public string DocumentContent { get; set; }
        public string DocumentUrl { get; set; }
        public string DocumentType { get; set; }
        public long DocumentSize { get; set; }
        public string DocumentName { get; set; }
        public bool IsUsingFile { get; set; }

        public int? CompanyTypeId { get; set; }
    }
}
