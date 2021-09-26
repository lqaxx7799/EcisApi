using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface ICompanyTypeRepository: IRepository<CompanyType>
    {
    }

    public class CompanyTypeRepository : Repository<CompanyType>, ICompanyTypeRepository
    {
        public CompanyTypeRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
