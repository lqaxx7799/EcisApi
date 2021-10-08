using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class Agent : BaseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }

        public int AccountId { get; set; }
        
        public virtual Account Account { get; set; }

        public virtual ICollection<VerificationProcess> VerificationProcesses { get; set; }
        public virtual ICollection<CompanyReport> CompanyActions { get; set; }
        public virtual ICollection<VerificationConfirmRequirement> VerificationConfirmRequirements { get; set; }
        public virtual ICollection<ViolationReport> ViolationReports { get; set; }

    }
}
