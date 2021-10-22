using EcisApi.Models;
using EcisApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Services
{
    public interface ICompanyReportDocumentService
    {
        ICollection<CompanyReportDocument> GetByReportId(int reportId);
    }
    public class CompanyReportDocumentService : ICompanyReportDocumentService
    {
        protected readonly ICompanyReportDocumentRepository companyReportDocumentRepository;

        public CompanyReportDocumentService(
            ICompanyReportDocumentRepository companyReportDocumentRepository
            )
        {
            this.companyReportDocumentRepository = companyReportDocumentRepository;
        }

        public ICollection<CompanyReportDocument> GetByReportId(int reportId)
        {
            return companyReportDocumentRepository.GetByReportId(reportId);
        }
    }
}
