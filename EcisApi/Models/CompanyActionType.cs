﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class CompanyActionType : BaseModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        public ICollection<CompanyAction> CompanyActions { get; set; }
    }
}
