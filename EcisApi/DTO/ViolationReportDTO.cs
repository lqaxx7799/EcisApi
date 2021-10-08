using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class ViolationReportDTO
    {
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public int ReportAgentId { get; set; }
        public ViolationReportDocument[] ViolationReportDocuments { get; set; }
    }
}
