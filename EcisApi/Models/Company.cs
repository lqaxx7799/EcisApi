using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class Company : BaseModel
    {
        public int ID { get; set; }
        public string CompanyNameVI { get; set; }
        public string CompanyNameEN { get; set; }
        public string CompanyCode { get; set; }
        public string LogoUrl { get; set; }

        public int AccountID { get; set; }
        public int RangerDistrictID { get; set; }
        public int CompanyTypeID { get; set; }

        public Account Account { get; set; }
        public CompanyType CompanyType { get; set; }
        public ICollection<CompanyAction> TargetedCompanyActions { get; set; }
        public ICollection<CompanyAction> CreatorCompanyActions { get; set; }
    }
}
