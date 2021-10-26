using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface ICriteriaDetailRepository : IRepository<CriteriaDetail>
    {

    }

    public class CriteriaDetailRepository : Repository<CriteriaDetail>, ICriteriaDetailRepository
    {
        public CriteriaDetailRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
