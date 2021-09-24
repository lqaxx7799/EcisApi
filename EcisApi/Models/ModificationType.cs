using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class ModificationType : BaseModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CompanyTypeModification> CompanyTypeModifications { get; set; }
    }
}
