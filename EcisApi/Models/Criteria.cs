using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class Criteria : BaseModel
    {
        public int Id { get; set; }
        public string CriteriaName { get; set; }
        public string Description { get; set; }

        public int? CriteriaTypeId { get; set; }

        public virtual CriteriaType CriteriaType { get; set; }

        public virtual ICollection<VerificationCriteria> VerificationCriterias { get; set; }
    }
}
