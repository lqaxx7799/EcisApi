﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class CriteriaDetail : BaseModel
    {
        public int Id { get; set; }
        public string CriteriaDetailName { get; set; }
        public string Description { get; set; }

        public int? CriteriaId { get; set; }

        public virtual Criteria Criteria { get; set; }

        public virtual ICollection<VerificationCriteria> VerificationCriterias { get; set; }
    }
}
