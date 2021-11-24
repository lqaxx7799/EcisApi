using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface IVerificationConfirmDocumentRepository : IRepository<VerificationConfirmDocument>
    {

    }

    public class VerificationConfirmDocumentRepository : Repository<VerificationConfirmDocument>, IVerificationConfirmDocumentRepository
    {
        public VerificationConfirmDocumentRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
