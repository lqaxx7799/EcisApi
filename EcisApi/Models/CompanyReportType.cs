using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class CompanyReportType : BaseModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        public ICollection<CompanyReport> CompanyReports { get; set; }
    }
}
