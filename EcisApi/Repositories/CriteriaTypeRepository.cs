using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface ICriteriaTypeRepository : IRepository<CriteriaType>
    {

    }

    public class CriteriaTypeRepository : Repository<CriteriaType>, ICriteriaTypeRepository
    {
        public CriteriaTypeRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
