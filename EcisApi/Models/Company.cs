using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class Company
    {
        public int ID { get; set; }
        public string CompanyNameVI { get; set; }
        public string CompanyNameEN { get; set; }
        public string CompanyCode { get; set; }
        public string LogoUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public int AccountID { get; set; }
        public int RangerDistrictID { get; set; }

        public Account Account { get; set; }
    }
}
