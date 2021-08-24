using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface ICriteriaRepository : IRepository<Criteria>
    {

    }

    public class CriteriaRepository : Repository<Criteria>, ICriteriaRepository
    {
        public CriteriaRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
