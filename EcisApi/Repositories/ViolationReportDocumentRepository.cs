using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface IViolationReportDocumentRepository : IRepository<ViolationReportDocument>
    {
        ICollection<ViolationReportDocument> GetByReportId(int reportId);
    }

    public class ViolationReportDocumentRepository : Repository<ViolationReportDocument>, IViolationReportDocumentRepository
    {
        public ViolationReportDocumentRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public ICollection<ViolationReportDocument> GetByReportId(int reportId)
        {
            return db.Set<ViolationReportDocument>().Where(x => x.ViolationReportId == reportId).ToList();
        }
    }
}
