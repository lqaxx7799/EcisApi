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
        CompanyType GetByName(string name);
    }

    public class CompanyTypeRepository : Repository<CompanyType>, ICompanyTypeRepository
    {
        public CompanyTypeRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public CompanyType GetByName(string name)
        {
            return db.Set<CompanyType>().Where(x => x.TypeName == name).FirstOrDefault();
        } 
    }
}
