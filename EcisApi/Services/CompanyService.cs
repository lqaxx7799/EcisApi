using EcisApi.DTO;
using EcisApi.Helpers;
using EcisApi.Models;
using EcisApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Services
{
    public interface ICompanyService
    {
        ICollection<Company> GetAll();
        ICollection<Company> GetAllActivated();
        Company GetById(int id);
        Company GetByAccountId(int accountId);
        ICollection<CompanyTypeModification> GetModificationReport(int month, int year);
        Task<dynamic> RegisterCompany(CompanyRegistrationDTO data);
        Task<Account> VerifyCompany(int accountId);
        Task<CompanyTypeModification> ModifyType(ModifyCompanyTypeDTO data);
    }

    public class CompanyService : ICompanyService
    {
        protected readonly IAccountRepository accountRepository;
        protected readonly ICompanyRepository companyRepository;
        protected readonly ICompanyTypeModificationRepository companyTypeModificationRepository;
        protected readonly IRoleRepository roleRepository;

        protected readonly IEmailHelper emailHelper;

        public CompanyService(
            IAccountRepository accountRepository,
            ICompanyRepository companyRepository,
            ICompanyTypeModificationRepository companyTypeModificationRepository,
            IRoleRepository roleRepository,

            IEmailHelper emailHelper
            )
        {
            this.accountRepository = accountRepository;
            this.companyRepository = companyRepository;
            this.companyTypeModificationRepository = companyTypeModificationRepository;
            this.roleRepository = roleRepository;

            this.emailHelper = emailHelper;
        }

        public ICollection<Company> GetAll()
        {
            return companyRepository.GetAll().ToList();
        }

        public ICollection<Company> GetAllActivated()
        {
            return companyRepository.GetAllActivated();
        }

        public Company GetById(int id)
        {
            return companyRepository.GetById(id);
        }

        public Company GetByAccountId(int accountId)
        {
            return companyRepository.GetByAccountId(accountId);
        }

        public ICollection<CompanyTypeModification> GetModificationReport(int month, int year)
        {
            return companyTypeModificationRepository.GetModificationReport(month, year);
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

            var company = new Company
            {
                CompanyCode = data.CompanyCode,
                CompanyNameEN = data.CompanyNameEN,
                CompanyNameVI = data.CompanyNameVI,
                AccountId = account.Id
            };
            await companyRepository.AddAsync(company);

            await emailHelper.SendEmailAsync(
                new string[] { data.Email },
                "Thông báo đăng ký doanh nghiệp thành công",
                EmailTemplate.CompanyRegistrationSuccess,
                new Dictionary<string, string>());

            return new
            {
                Company = company,
                Account = account,
            };
        }

        public async Task<Account> VerifyCompany(int accountId)
        {
            var account = accountRepository.GetById(accountId);
            if (account == null)
            {
                throw new BadHttpRequestException("Tài khoản không tồn tại");
            }

            var rawPassword = Guid.NewGuid().ToString();
            account.Password = CommonUtils.GenerateSHA1(rawPassword);
            account.IsVerified = true;

            await accountRepository.UpdateAsync(account);

            var company = companyRepository.GetByAccountId(accountId);

            var mailParams = new Dictionary<string, string>
            {
                { "companyName", company.CompanyNameVI },
                { "email", account.Email },
                { "password", rawPassword }
            };

            await emailHelper.SendEmailAsync(
                new string[] { account.Email },
                "Thông tin tài khoản đăng nhập",
                EmailTemplate.CompanyRegistrationVerified,
                mailParams);

            return account;
        }

        public async Task<CompanyTypeModification> ModifyType(ModifyCompanyTypeDTO data)
        {
            Company company = companyRepository.GetById(data.CompanyId);
            if (company == null)
            {
                return null;
            }

            CompanyTypeModification currentModification = new()
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
