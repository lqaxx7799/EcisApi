using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class ModifyCompanyTypeDTO
    {
        public int CompanyId { get; set; }
        public int CompanyTypeId { get; set; }
        public string ModificationType { get; set; }
        public int ModificationTargetId { get; set; }
    }
}
