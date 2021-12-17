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
    public interface IV1Service
    {
        ThirdParty GetById(int id);

        ICollection<PublicCompanyTypeModificationDTO> GetModificationReport(int month, int year);
        ICollection<PublicCompanyTypeModificationDTO> GetModificationReportByCompanyId(int id);

        ICollection<PublicCompanyDTO> GetCompanies();
        PublicCompanyDTO GetCompanyById(int id);
    }

    public class V1Service : IV1Service
    {
        private readonly ICompanyRepository companyRepository;
        private readonly ICompanyTypeModificationRepository companyTypeModificationRepository;
        private readonly IThirdPartyRepository thirdPartyRepository;

        public V1Service(
            ICompanyRepository companyRepository,
            ICompanyTypeModificationRepository companyTypeModificationRepository,
            IThirdPartyRepository thirdPartyRepository)
        {
            this.companyRepository = companyRepository;
            this.companyTypeModificationRepository = companyTypeModificationRepository;
            this.thirdPartyRepository = thirdPartyRepository;
        }

        public ThirdParty GetById(int id)
        {
            return thirdPartyRepository.GetById(id);
        }

        public ICollection<PublicCompanyTypeModificationDTO> GetModificationReport(int month, int year)
        {
            var data = companyTypeModificationRepository.GetModificationReport(month, year);
            var result = data.Select(x => new PublicCompanyTypeModificationDTO
            {
                Id = x.Id,
                AnnouncedAt = x.AnnouncedAt,
                CompanyId = x.CompanyId,
                ModificationType = x.Modification,
                PreviousCompanyType = x.PreviousCompanyType.TypeName,
                UpdatedCompanyType = x.UpdatedCompanyType.TypeName
            })
                .ToList();
            return result;
        }

        public ICollection<PublicCompanyTypeModificationDTO> GetModificationReportByCompanyId(int id)
        {
            var data = companyTypeModificationRepository.GetCompanyModificationReport(id);
            var result = data.Select(x => new PublicCompanyTypeModificationDTO
            {
                Id = x.Id,
                AnnouncedAt = x.AnnouncedAt,
                CompanyId = x.CompanyId,
                ModificationType = x.Modification,
                PreviousCompanyType = x.PreviousCompanyType.TypeName,
                UpdatedCompanyType = x.UpdatedCompanyType.TypeName
            })
                .ToList();
            return result;
        }

        public ICollection<PublicCompanyDTO> GetCompanies()
        {
            var companies = companyRepository.GetAllActivated();
            var result = companies.Select(x => new PublicCompanyDTO
            {
                Id = x.Id,
                CompanyCode = x.CompanyCode,
                CompanyNameEN = x.CompanyNameEN,
                CompanyNameVI = x.CompanyNameVI,
                CompanyType = x.CompanyType.TypeName,
                Email = x.Account.Email,
                LogoUrl = x.LogoUrl,
                CreatedAt = x.CreatedAt
            }).ToList();
            return result;
        }

        public PublicCompanyDTO GetCompanyById(int id)
        {
            var company = companyRepository.GetById(id);
            if (company == null || company.IsDeleted || !company.Account.IsVerified || company.Account.IsDeleted)
            {
                throw new BadHttpRequestException("InvalidCompany");
            }
            return new()
            {
                Id = company.Id,
                CompanyCode = company.CompanyCode,
                CompanyNameEN = company.CompanyNameEN,
                CompanyNameVI = company.CompanyNameVI,
                CompanyType = company.CompanyType.TypeName,
                Email = company.Account.Email,
                LogoUrl = company.LogoUrl,
                CreatedAt = company.CreatedAt
            };
        }
    }
}
