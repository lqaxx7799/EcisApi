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
    public interface ICompanyReportService
    {
        ICollection<CompanyReport> GetAll();
        ICollection<CompanyReport> GetPending();
        bool CanCreateReport(int companyId);
        CompanyReport GetById(int id);
        Task<CompanyReport> AddAsync(CompanyReportDTO payload);
        Task<CompanyReport> ApproveAsync(int id);
        Task<CompanyReport> RejectAsync(int id);
    }

    public class CompanyReportService : ICompanyReportService
    {
        protected readonly ICompanyReportRepository companyReportRepository;
        protected readonly ICompanyReportDocumentRepository companyReportDocumentRepository;

        protected readonly IVerificationProcessService verificationProcessService;


        public CompanyReportService(
            ICompanyReportRepository companyReportRepository,
            ICompanyReportDocumentRepository companyReportDocumentRepository,
            IVerificationProcessService verificationProcessService
            )
        {
            this.companyReportRepository = companyReportRepository;
            this.companyReportDocumentRepository = companyReportDocumentRepository;

            this.verificationProcessService = verificationProcessService;
        }

        public ICollection<CompanyReport> GetAll()
        {
            return companyReportRepository.GetAll().ToList();
        }
        
        public ICollection<CompanyReport> GetPending()
        {
            return companyReportRepository.GetPending();
        }

        public bool CanCreateReport(int companyId)
        {
            var report = companyReportRepository.GetCurrentUnhandled(companyId);
            if (report != null)
            {
                return false;
            }
            var process = verificationProcessService.GetCompanyLast(companyId);
            if (process != null && process.Status != AppConstants.VerificationProcessStatus.Finished)
            {
                return false;
            }
            return true;
        }

        public CompanyReport GetById(int id)
        {
            return companyReportRepository.GetById(id);
        }

        public async Task<CompanyReport> AddAsync(CompanyReportDTO payload)
        {
            CompanyReport companyReport = new()
            {
                Status = AppConstants.CompanyReportStatus.PENDING,
                ActionTitle = payload.ActionTitle,
                Description = payload.Description,
                CompanyReportTypeId = payload.CompanyReportTypeId,
                CreatorCompanyId = payload.CreatorCompanyId,
                TargetedCompanyId = payload.TargetedCompanyId,
                VerificationProcessId = payload.VerificationProcessId
            };
            var createdCompanyReport = await companyReportRepository.AddAsync(companyReport);
            foreach(var document in payload.CompanyReportDocuments)
            {
                document.CompanyReportId = createdCompanyReport.Id;
                await companyReportDocumentRepository.AddAsync(document);
            }

            return createdCompanyReport;
        }

        public async Task<CompanyReport> ApproveAsync(int id)
        {
            var report = companyReportRepository.GetById(id);
            if (report == null || report.TargetedCompanyId == null)
            {
                throw new BadHttpRequestException("CompanyReportNotFound");
            }
            if (report.Status != AppConstants.CompanyReportStatus.PENDING)
            {
                throw new BadHttpRequestException("CompanyReportInvalid");
            }

            report.Status = AppConstants.CompanyReportStatus.APPROVED;
            report.IsHandled = true;
            report.HandledAt = DateTime.Now;
            var updatedReport = await companyReportRepository.UpdateAsync(report);

            await verificationProcessService.GenerateAsync(report.TargetedCompanyId.Value);

            return updatedReport;
        }

        public async Task<CompanyReport> RejectAsync(int id)
        {
            var report = companyReportRepository.GetById(id);
            if (report == null)
            {
                throw new BadHttpRequestException("CompanyReportNotFound");
            }
            if (report.Status != AppConstants.CompanyReportStatus.PENDING)
            {
                throw new BadHttpRequestException("CompanyReportInvalid");
            }

            report.Status = AppConstants.CompanyReportStatus.REJECTED;
            report.IsHandled = true;
            report.HandledAt = DateTime.Now;
            return await companyReportRepository.UpdateAsync(report);
        }
    }
}
