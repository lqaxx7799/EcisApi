using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface IVerificationProcessRepository : IRepository<VerificationProcess>
    {

    }

    public class VerificationProcessRepository : Repository<VerificationProcess>, IVerificationProcessRepository
    {
        public VerificationProcessRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
