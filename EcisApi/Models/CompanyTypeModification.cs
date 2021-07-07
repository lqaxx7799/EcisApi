using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class CompanyTypeModification : BaseModel
    {
        public int Id { get; set; }
        
        public int CompanyId { get; set; }
        public int PreviousCompanyTypeId { get; set; }
        public int UpdatedCompanyTypeId { get; set; }
        public int ModificationTypeId { get; set; }
        public int VerificationProcessId { get; set; }
        public int CompanyActionId { get; set; }

        public Company Company { get; set; }
        public CompanyType PreviousCompanyType { get; set; }
        public CompanyType UpdatedCompanyType { get; set; }
        public ModificationType ModificationType { get; set; }
        public VerificationProcess VerificationProcess { get; set; }
        public CompanyAction CompanyAction { get; set; }
    }
}
