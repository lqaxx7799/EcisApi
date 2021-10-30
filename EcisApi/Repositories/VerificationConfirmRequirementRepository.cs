using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface IVerificationConfirmRequirementRepository : IRepository<VerificationConfirmRequirement>
    {
        ICollection<VerificationConfirmRequirement> GetPendingByAgentId(int agentId);
        ICollection<VerificationConfirmRequirement> GetFinishedByAgentId(int agentId);
        ICollection<VerificationConfirmRequirement> GetPendingByCompanyId(int companyId);
        VerificationConfirmRequirement GetOneByProcessId(int processId);
    }

    public class VerificationConfirmRequirementRepository : Repository<VerificationConfirmRequirement>, IVerificationConfirmRequirementRepository
    {
        public VerificationConfirmRequirementRepository(DataContext context) : base(context)
        {

        }

        public ICollection<VerificationConfirmRequirement> GetPendingByAgentId(int agentId)
        {
            return db.Set<VerificationConfirmRequirement>().Where(x => x.AssignedAgentId == agentId && x.ConfirmedAt == null).ToList();
        }

        public ICollection<VerificationConfirmRequirement> GetFinishedByAgentId(int agentId)
        {
            return db.Set<VerificationConfirmRequirement>().Where(x => x.AssignedAgentId == agentId && x.ConfirmedAt != null).ToList();
        }

        public ICollection<VerificationConfirmRequirement> GetPendingByCompanyId(int companyId)
        {
            return db.Set<VerificationConfirmRequirement>().Where(x => 
                x.VerificationProcess.CompanyId == companyId && 
                x.AnnouncedCompanyAt != null &&
                x.ConfirmedAt == null
                ).ToList();
        }

        public VerificationConfirmRequirement GetOneByProcessId(int processId)
        {
            return db.Set<VerificationConfirmRequirement>().Where(x => x.VerificationProcessId == processId).FirstOrDefault();
        }
    }
}
