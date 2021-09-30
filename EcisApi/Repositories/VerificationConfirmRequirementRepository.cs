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
        VerificationConfirmRequirement GetOneByProcessId(int processId);
    }

    public class VerificationConfirmRequirementRepository : Repository<VerificationConfirmRequirement>, IVerificationConfirmRequirementRepository
    {
        public VerificationConfirmRequirementRepository(DataContext context) : base(context)
        {

        }

        public VerificationConfirmRequirement GetOneByProcessId(int processId)
        {
            return db.Set<VerificationConfirmRequirement>().Where(x => x.VerificationProcessId == processId).FirstOrDefault();
        }
    }
}
