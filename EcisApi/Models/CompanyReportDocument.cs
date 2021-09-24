using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class CompanyReportDocument : BaseModel
    {
        public int Id { get; set; }
        public string ResourceType { get; set; }
        public string ResourceUrl { get; set; }

        public int? CompanyReportId { get; set; }

        public virtual CompanyReport CompanyReport { get; set; }
    }
}
