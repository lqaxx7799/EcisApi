using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class CompanyReportDocument : BaseModel
    {
        public int Id { get; set; }
        public string DocumentType { get; set; }
        public string DocumentUrl { get; set; }
        public string DocumentName { get; set; }
        public long DocumentSize { get; set; }

        public int? CompanyReportId { get; set; }

        public virtual CompanyReport CompanyReport { get; set; }
    }
}
