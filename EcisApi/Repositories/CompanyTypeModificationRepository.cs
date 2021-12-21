using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface ICompanyTypeModificationRepository : IRepository<CompanyTypeModification>
    {
        ICollection<CompanyTypeModification> GetModificationReport(int month, int year);
        ICollection<CompanyTypeModification> GetModificationReportPrivate(int month, int year);
        ICollection<CompanyTypeModification> GetCompanyModificationReport(int companyId);
        CompanyTypeModification GetLatestByCompanyId(int companyId);
    }

    public class CompanyTypeModificationRepository : Repository<CompanyTypeModification>, ICompanyTypeModificationRepository
    {
        public CompanyTypeModificationRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public ICollection<CompanyTypeModification> GetModificationReport(int month, int year)
        {
            return db.Set<CompanyTypeModification>().Where(x =>
                x.CreatedAt.Month == month &&
                x.CreatedAt.Year == year &&
                !x.IsDeleted
                ).ToList();
        }

        public ICollection<CompanyTypeModification> GetModificationReportPrivate(int month, int year)
        {
            return db.Set<CompanyTypeModification>().Where(x =>
                x.CreatedAt.Month == month &&
                x.CreatedAt.Year == year &&
                !x.IsDeleted
                ).ToList();
        }

        public ICollection<CompanyTypeModification> GetCompanyModificationReport(int companyId)
        {
            return db.Set<CompanyTypeModification>()
                .Where(x => x.CompanyId == companyId && !x.IsDeleted)
                .ToList();
        }

        public CompanyTypeModification GetLatestByCompanyId(int companyId)
        {
            return db.Set<CompanyTypeModification>().OrderBy(x => x.CreatedAt).FirstOrDefault();
        }
    }
}
