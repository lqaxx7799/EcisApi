﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class Role : BaseModel
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
