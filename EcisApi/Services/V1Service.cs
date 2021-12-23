using EcisApi.DTO;
using EcisApi.Helpers;
using EcisApi.Models;
using EcisApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Services
{
    public interface IV1Service
    {
        PublicV1ThirdPartyDTO GetById(int id);

        ICollection<PublicCompanyTypeModificationDTO> GetModificationReport(int month, int year);
        ICollection<PublicCompanyTypeModificationDTO> GetModificationReportByCompanyId(int id);

        ICollection<PublicCompanyDTO> GetCompanies();
        PublicCompanyDTO GetCompanyById(int id);
        PublicCompanyDTO GetCompanyByCode(string code);

        PublicV1AuthenticateResponseDTO Authenticate(PublicV1AuthenticateDTO payload);
    }

    public class V1Service : IV1Service
    {
        private readonly IAccountRepository accountRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly ICompanyTypeModificationRepository companyTypeModificationRepository;
        private readonly IThirdPartyRepository thirdPartyRepository;

        private readonly AppSettings appSettings;

        public V1Service(
            IAccountRepository accountRepository,
            ICompanyRepository companyRepository,
            ICompanyTypeModificationRepository companyTypeModificationRepository,
            IThirdPartyRepository thirdPartyRepository,
            IOptions<AppSettings> appSettings)
        {
            this.accountRepository = accountRepository;
            this.companyRepository = companyRepository;
            this.companyTypeModificationRepository = companyTypeModificationRepository;
            this.thirdPartyRepository = thirdPartyRepository;
            this.appSettings = appSettings.Value;
        }

        public PublicV1ThirdPartyDTO GetById(int id)
        {
            var thirdParty = thirdPartyRepository.GetById(id);
            return new PublicV1ThirdPartyDTO()
            {
                Id = thirdParty.Id,
                ClientId = thirdParty.ClientId,
                ClientSecret = thirdParty.ClientSecret,
                CreatedAt = thirdParty.CreatedAt,
                Email = thirdParty.Account?.Email,
                IsActive = thirdParty.IsActive,
                UserName = thirdParty.UserName
            };
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
                PreviousCompanyType = x.PreviousCompanyType?.TypeName,
                UpdatedCompanyType = x.UpdatedCompanyType?.TypeName
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
                PreviousCompanyType = x.PreviousCompanyType?.TypeName,
                UpdatedCompanyType = x.UpdatedCompanyType?.TypeName
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
                CompanyType = x.CompanyType?.TypeName,
                Email = x.Account?.Email,
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
                CompanyType = company.CompanyType?.TypeName,
                Email = company.Account?.Email,
                LogoUrl = company.LogoUrl,
                CreatedAt = company.CreatedAt
            };
        }

        public PublicCompanyDTO GetCompanyByCode(string code)
        {
            var company = companyRepository.GetByCompanyCode(code);
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
                CompanyType = company.CompanyType?.TypeName,
                Email = company.Account?.Email,
                LogoUrl = company.LogoUrl,
                CreatedAt = company.CreatedAt
            };
        }

        public PublicV1AuthenticateResponseDTO Authenticate(PublicV1AuthenticateDTO payload)
        {
            var account = accountRepository.GetByEmail(payload.Email);
            if (account == null || !account.IsVerified || account.IsDeleted)
            {
                throw new BadHttpRequestException("InvalidAccount");
            }
            if (account.Role.RoleName != "ThirdParty")
            {
                throw new BadHttpRequestException("InvalidAccount");
            }
            var thirdParty = account.ThirdParty;
            if (thirdParty == null)
            {
                throw new BadHttpRequestException("InvalidThirdParty");
            }
            if (thirdParty.ClientSecret != payload.ClientSecret || thirdParty.ClientId != payload.ClientId)
            {
                throw new BadHttpRequestException("IncorrectThirdPartyInformation");
            }
            if (!thirdParty.IsActive)
            {
                throw new BadHttpRequestException("ThirdPartyDeactivated");
            }

            var token = CommonUtils.GenerateV1JwtToken(account, payload.ClientSecret, payload.ClientId, appSettings.Secret);
            return new PublicV1AuthenticateResponseDTO
            {
                AccessToken = token
            };
        }
    }
}
