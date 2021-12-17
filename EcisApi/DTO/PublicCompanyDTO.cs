using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class PublicCompanyDTO
    {
        public int Id { get; set; }
        public string CompanyNameVI { get; set; }
        public string CompanyNameEN { get; set; }
        public string CompanyCode { get; set; }
        public string LogoUrl { get; set; }
        public string Email { get; set; }
        public string CompanyType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
