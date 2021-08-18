using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface IVerificationProcessRepository : IRepository<VerificationProcess>
    {
        ICollection<VerificationProcess> GetByCompany(int companyId);
    }

    public class VerificationProcessRepository : Repository<VerificationProcess>, IVerificationProcessRepository
    {
        public VerificationProcessRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public ICollection<VerificationProcess> GetByCompany(int companyId)
        {
            return db.Set<VerificationProcess>().Where(x => x.CompanyId == companyId).ToList();
        }
    }
}
