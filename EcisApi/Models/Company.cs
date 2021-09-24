﻿using Newtonsoft.Json;
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
        public virtual Account Account { get; set; }
        public virtual CompanyType CompanyType { get; set; }
        public virtual ICollection<CompanyReport> TargetedCompanyReports { get; set; }
        public virtual ICollection<CompanyReport> CreatorCompanyReports { get; set; }
        public virtual ICollection<CompanyTypeModification> CompanyTypeModifications { get; set; }
        public virtual ICollection<VerificationProcess> VerificationProcesses { get; set; } 
    }
}
