using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface ICompanyTypeModificationRepository : IRepository<CompanyTypeModification>
    {
        CompanyTypeModification GetLatestByCompanyId(int companyId);
    }

    public class CompanyTypeModificationRepository : Repository<CompanyTypeModification>, ICompanyTypeModificationRepository
    {
        public CompanyTypeModificationRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public CompanyTypeModification GetLatestByCompanyId(int companyId)
        {
            return db.Set<CompanyTypeModification>().OrderBy(x => x.CreatedAt).FirstOrDefault();
        }
    }
}
