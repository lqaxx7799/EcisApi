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
        Company GetByAccountId(int accountId);
        Company GetByCompanyCode(string companyCode);
    }


    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public Company GetByAccountId(int accountId)
        {
            return db.Set<Company>().Where(x => x.AccountId == accountId).FirstOrDefault();
        }

        public Company GetByCompanyCode(string companyCode)
        {
            return db.Set<Company>().Where(x => x.CompanyCode == companyCode).FirstOrDefault();
        }

    }
}
