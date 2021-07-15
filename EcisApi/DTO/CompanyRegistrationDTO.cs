using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class CompanyRegistrationDTO
    {
        public string Email { get; set; }
        public string CompanyNameVI { get; set; }
        public string CompanyNameEN { get; set; }
        public string CompanyCode { get; set; }
        public string LogoUrl { get; set; }
    }
}
