using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface ICompanyReportDocumentRepository : IRepository<CompanyReportDocument>
    {
        ICollection<CompanyReportDocument> GetByReportId(int reportId);
    }

    public class CompanyReportDocumentRepository : Repository<CompanyReportDocument>, ICompanyReportDocumentRepository
    {
        public CompanyReportDocumentRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public ICollection<CompanyReportDocument> GetByReportId(int reportId)
        {
            return db.Set<CompanyReportDocument>().Where(x => x.CompanyReportId == reportId && !x.IsDeleted).ToList();
        }
    }
}
