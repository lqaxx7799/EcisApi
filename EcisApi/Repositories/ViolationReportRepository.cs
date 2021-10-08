using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface IViolationReportRepository : IRepository<ViolationReport>
    {

    }

    public class ViolationReportRepository : Repository<ViolationReport>, IViolationReportRepository
    {
        public ViolationReportRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
