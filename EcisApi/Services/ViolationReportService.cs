using EcisApi.DTO;
using EcisApi.Helpers;
using EcisApi.Models;
using EcisApi.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Services
{
    public interface IViolationReportService
    {
        ICollection<ViolationReport> GetAll();
        ViolationReport GetById(int id);
        Task<ViolationReport> AddAsync(ViolationReportDTO payload);
        Task<ViolationReport> ApproveAsync(int id);
        Task<ViolationReport> RejectAsync(int id);
    }

    public class ViolationReportService : IViolationReportService
    {
        protected readonly ICompanyRepository companyRepository;
        protected readonly ICompanyTypeRepository companyTypeRepository;
        protected readonly ICompanyTypeModificationRepository companyTypeModificationRepository;
        protected readonly IViolationReportRepository violationReportRepository;
        protected readonly IViolationReportDocumentRepository violationReportDocumentRepository;

        protected readonly IEmailHelper emailHelper;

        public ViolationReportService(
            ICompanyRepository companyRepository,
            ICompanyTypeRepository companyTypeRepository,
            ICompanyTypeModificationRepository companyTypeModificationRepository,
            IViolationReportRepository violationReportRepository,
            IViolationReportDocumentRepository violationReportDocumentRepository,
            IEmailHelper emailHelper
            )
        {
            this.companyRepository = companyRepository;
            this.companyTypeRepository = companyTypeRepository;
            this.companyTypeModificationRepository = companyTypeModificationRepository;
            this.violationReportRepository = violationReportRepository;
            this.violationReportDocumentRepository = violationReportDocumentRepository;
            this.emailHelper = emailHelper;
        }

        public ICollection<ViolationReport> GetAll()
        {
            return violationReportRepository.GetAll().ToList();
        }

        public ViolationReport GetById(int id)
        {
            return violationReportRepository.GetById(id);
        }

        public async Task<ViolationReport> AddAsync(ViolationReportDTO payload)
        {
            ViolationReport report = new()
            {
                Description = payload.Description,
                CompanyId = payload.CompanyId,
                ReportAgentId = payload.ReportAgentId,
                Status = AppConstants.ViolationReportStatus.PENDING
            };
            var createdReport = await violationReportRepository.AddAsync(report);
            foreach (var item in payload.ViolationReportDocuments)
            {
                ViolationReportDocument document = new()
                {
                    DocumentName = item.DocumentName,
                    DocumentSize = item.DocumentSize,
                    DocumentType = item.DocumentType,
                    DocumentUrl = item.DocumentUrl,
                    ViolationReportId = createdReport.Id
                };
                await violationReportDocumentRepository.AddAsync(document);
            }
            return createdReport;
        }

        public async Task<ViolationReport> ApproveAsync(int id) {
            var report = violationReportRepository.GetById(id);
            if (report == null)
            {
                throw new BadHttpRequestException("ViolationReportNotFound");
            }
            if (report.Status != AppConstants.ViolationReportStatus.PENDING)
            {
                throw new BadHttpRequestException("ViolationReportInvalid");
            }

            report.Status = AppConstants.ViolationReportStatus.APPROVED;
            report.ApprovedAt = DateTime.Now;
            await violationReportRepository.UpdateAsync(report);

            Company company = companyRepository.GetById(report.CompanyId);
            var companyType = companyTypeRepository.GetByName("Loại 2");

            CompanyTypeModification currentModification = new()
            {
                CompanyId = report.CompanyId,
                PreviousCompanyTypeId = company.CompanyTypeId,
                UpdatedCompanyTypeId = companyType.Id,
                Modification = AppConstants.CompanyModificationType.VIOLATION,
                ViolationReportId = report.Id,
            };
            await companyTypeModificationRepository.AddAsync(currentModification);

            company.CompanyTypeId = companyType.Id;
            await companyRepository.UpdateAsync(company);

            try
            {
                await emailHelper.SendEmailAsync(
                   new string[] { company.Account.Email },
                   "Doanh nghiệp của bạn đã vi phạm và bị hạ đánh giá",
                   EmailTemplate.VerificationFinished,
                   new Dictionary<string, string>());
            }
            catch (Exception)
            {

            }

            return report;
        }

        public async Task<ViolationReport> RejectAsync(int id)
        {
            var report = violationReportRepository.GetById(id);
            if (report == null)
            {
                throw new BadHttpRequestException("ViolationReportNotFound");
            }
            if (report.Status != AppConstants.ViolationReportStatus.PENDING)
            {
                throw new BadHttpRequestException("ViolationReportInvalid");
            }

            report.Status = AppConstants.ViolationReportStatus.REJECTED;
            return await violationReportRepository.UpdateAsync(report);
        }
    }
}
