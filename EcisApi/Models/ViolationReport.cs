using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class ViolationReport : BaseModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? ApprovedAt { get; set; }

        public int? CompanyId { get; set; }
        public int? ReportAgentId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Agent ReportAgent { get; set; }

        [JsonIgnore]
        public virtual ICollection<ViolationReportDocument> ViolationReportDocuments { get; set; }
    }
}
