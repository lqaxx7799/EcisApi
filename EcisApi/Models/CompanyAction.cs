using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class CompanyAction : BaseModel
    {
        public int ID { get; set; }
        public string ActionTitle { get; set; }
        public string Description { get; set; }
        public DateTime AcceptedAt { get; set; }
        public DateTime HandledAt { get; set; }
        public bool IsHandled { get; set; }

        public int VerificationProcessID { get; set; }
        public int CompanyActionTypeID { get; set; }
        public int TargetedCompanyID { get; set; }
        public int CreatorCompanyID { get; set; }
        public int AssignedAgentID { get; set; }

        public VerificationProcess VerificationProcess { get; set; }
        public CompanyActionType CompanyActionType { get; set; }
        public Company TargetedCompany { get; set; }
        public Company CreatorCompany { get; set; }
        public Agent AssignedAgent { get; set; }
    }
}
