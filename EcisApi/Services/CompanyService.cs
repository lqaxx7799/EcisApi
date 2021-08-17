using EcisApi.DTO;
using EcisApi.Helpers;
using EcisApi.Models;
using EcisApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Services
{
    public interface ICompanyService
    {
        Company GetById(int id);
        Task<dynamic> RegisterCompany(CompanyRegistrationDTO data);
        Task<CompanyTypeModification> ModifyType(ModifyCompanyTypeDTO data);
    }

    public class CompanyService : ICompanyService
    {
        protected readonly IAccountRepository accountRepository;
        protected readonly ICompanyRepository companyRepository;
        protected readonly ICompanyTypeModificationRepository companyTypeModificationRepository;
        protected readonly IRoleRepository roleRepository;

        public CompanyService(
            IAccountRepository accountRepository,
            ICompanyRepository companyRepository,
            ICompanyTypeModificationRepository companyTypeModificationRepository,
            IRoleRepository roleRepository
            )
        {
            this.accountRepository = accountRepository;
            this.companyRepository = companyRepository;
            this.companyTypeModificationRepository = companyTypeModificationRepository;
            this.roleRepository = roleRepository;
        }

        public Company GetById(int id)
        {
            return companyRepository.GetById(id);
        }

        public async Task<dynamic> RegisterCompany(CompanyRegistrationDTO data)
        {
            // validate
            var existedAccount = accountRepository.GetByEmail(data.Email);
            if (existedAccount != null)
            {
                throw new ArgumentException("Email đã tồn tại trong hệ thống");
            }

            var existedCompany = companyRepository.GetByCompanyCode(data.CompanyCode);
            if (existedCompany != null)
            {
                throw new ArgumentException("Mã doanh nghiệp đã tồn tại trong hệ thống");
            }

            var role = roleRepository.GetRoleByName("COMPANY");
            if (role == null)
            {
                throw new Exception("Lỗi: không tồn tại role trong hệ thống");
            }

            var rawPassword = Guid.NewGuid().ToString();
            var account = new Account
            {
                Email = data.Email,
                Password = CommonUtils.GenerateSHA1(rawPassword),
                IsVerified = false,
                RoleId = role.Id,
            };
            await accountRepository.AddAsync(account);

            // upload file

            var company = new Company
            {
                CompanyCode = data.CompanyCode,
                CompanyNameEN = data.CompanyNameEN,
                CompanyNameVI = data.CompanyNameVI,
                AccountId = account.Id,
                LogoUrl = data.LogoUrl
            };
            await companyRepository.AddAsync(company);

            // TODO: send mail

            return new
            {
                Company = company,
                Account = account,
            };
        }

        public async Task<CompanyTypeModification> ModifyType(ModifyCompanyTypeDTO data)
        {
            Company company = companyRepository.GetById(data.CompanyId);
            if (company == null)
            {
                return null;
            }

            CompanyTypeModification currentModification = new CompanyTypeModification
            {
                CompanyId = data.CompanyId,
                PreviousCompanyTypeId = company.CompanyTypeId,
                UpdatedCompanyTypeId = data.CompanyTypeId
            };
            if (data.ModificationType == "VERIFICATION")
            {
                currentModification.VerificationProcessId = data.ModificationTargetId;
            } 
            else if (data.ModificationType == "REPORT")
            {
                currentModification.CompanyReportId = data.ModificationTargetId;
            }
            await companyTypeModificationRepository.AddAsync(currentModification);

            company.CompanyTypeId = data.CompanyTypeId;
            await companyRepository.UpdateAsync(company);

            return currentModification;
        }
    }
}
