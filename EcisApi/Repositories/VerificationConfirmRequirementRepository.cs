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
        ICollection<VerificationConfirmRequirement> GetByAgentId(int agentId);
        ICollection<VerificationConfirmRequirement> GetPendingByCompanyId(int companyId);
        VerificationConfirmRequirement GetOneByProcessId(int processId);
    }

    public class VerificationConfirmRequirementRepository : Repository<VerificationConfirmRequirement>, IVerificationConfirmRequirementRepository
    {
        public VerificationConfirmRequirementRepository(DataContext context) : base(context)
        {

        }

        public ICollection<VerificationConfirmRequirement> GetByAgentId(int agentId)
        {
            return db.Set<VerificationConfirmRequirement>().Where(x => x.AssignedAgentId == agentId).ToList();
        }

        public ICollection<VerificationConfirmRequirement> GetPendingByCompanyId(int companyId)
        {
            return db.Set<VerificationConfirmRequirement>().Where(x => 
                x.VerificationProcess.CompanyId == companyId && 
                x.AnnouncedCompanyAt != null &&
                x.ConfirmDocumentUrl == null
                ).ToList();
        }

        public VerificationConfirmRequirement GetOneByProcessId(int processId)
        {
            return db.Set<VerificationConfirmRequirement>().Where(x => x.VerificationProcessId == processId).FirstOrDefault();
        }
    }
}
