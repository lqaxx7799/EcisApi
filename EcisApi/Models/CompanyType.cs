using Newtonsoft.Json;
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

        [JsonIgnore]
        public virtual ICollection<Company> Companies { get; set; }
        [JsonIgnore]
        public virtual ICollection<VerificationProcess> VerificationProcesses { get; set; }
        [JsonIgnore]
        public virtual ICollection<CompanyTypeModification> PreviousCompanyTypeModifications { get; set; }
        [JsonIgnore]
        public virtual ICollection<CompanyTypeModification> UpdatedCompanyTypeModifications { get; set; }
        [JsonIgnore]
        public virtual ICollection<VerificationConfirmRequirement> VerificationConfirmRequirements { get; set; }
    }
}
