using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface ICompanyRepository: IRepository<Company>
    {
 
    }


    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(DataContext dataContext) : base(dataContext)
        {

        }

    }
}
