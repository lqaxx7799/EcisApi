using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class CompanyActionAttachment : BaseModel
    {
        public int Id { get; set; }
        public string ResourceType { get; set; }
        public string ResourceUrl { get; set; }

        public int CompanyActionId { get; set; }

        public CompanyAction CompanyAction { get; set; }
    }
}
