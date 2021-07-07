using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class CompanyType : BaseModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        public ICollection<Company> Companies { get; set; }
        public ICollection<VerificationProcess> VerificationProcesses { get; set; }
        public ICollection<CompanyTypeModification> PreviousCompanyTypeModifications { get; set; }
        public ICollection<CompanyTypeModification> UpdatedCompanyTypeModifications { get; set; }
    }
}
