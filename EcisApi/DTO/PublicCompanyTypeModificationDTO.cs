using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class PublicCompanyTypeModificationDTO
    {
        public int Id { get; set; }

        public string ModificationType { get; set; }
        public DateTime? AnnouncedAt { get; set; }

        public int? CompanyId { get; set; }
        public string PreviousCompanyType { get; set; }
        public string UpdatedCompanyType { get; set; }
    }
}
