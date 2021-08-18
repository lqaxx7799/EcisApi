using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface IVerificationDocumentRepository : IRepository<VerificationDocument>
    {

    }

    public class VerificationDocumentRepository : Repository<VerificationDocument>, IVerificationDocumentRepository
    {
        public VerificationDocumentRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
