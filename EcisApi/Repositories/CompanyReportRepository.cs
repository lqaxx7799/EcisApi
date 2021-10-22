using EcisApi.Data;
using EcisApi.Helpers;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface ICompanyReportRepository : IRepository<CompanyReport>
    {
        ICollection<CompanyReport> GetPending();
    }

    public class CompanyReportRepository : Repository<CompanyReport>, ICompanyReportRepository
    {
        public CompanyReportRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public ICollection<CompanyReport> GetPending()
        {
            return db.Set<CompanyReport>().Where(x => x.Status == AppConstants.CompanyReportStatus.PEDNING && !x.IsDeleted).ToList();
        }

    }
}
