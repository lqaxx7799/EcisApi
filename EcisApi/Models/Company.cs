using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class Company : BaseModel
    {
        public int Id { get; set; }
        public string CompanyNameVI { get; set; }
        public string CompanyNameEN { get; set; }
        public string CompanyCode { get; set; }
        public string LogoUrl { get; set; }

        public int AccountId { get; set; }
        public int? CompanyTypeId { get; set; }
        //public int RangerDistrictId { get; set; }

        [JsonIgnore]
        public Account Account { get; set; }
        public CompanyType CompanyType { get; set; }
        public ICollection<CompanyAction> TargetedCompanyActions { get; set; }
        public ICollection<CompanyAction> CreatorCompanyActions { get; set; }
        public ICollection<CompanyTypeModification> CompanyTypeModifications { get; set; }
        public ICollection<VerificationProcess> VerificationProcesses { get; set; } 
    }
}
