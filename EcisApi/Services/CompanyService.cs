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
        ICollection<CompanyTypeModification> GetModificationReportPrivate(int month, int year);
        ICollection<CompanyTypeModification> GetCompanyModificationReport(int companyId);
        CompanyTypeModification GetModificationById(int id);
        Task<Company> RegisterCompany(CompanyRegistrationDTO data);
        Task<Account> VerifyCompany(int accountId);
        Task<CompanyTypeModification> ModifyType(ModifyCompanyTypeDTO data);
        Task<CompanyTypeModification> UpdateModificationAsync(CompanyTypeModification payload);
    }

    public class CompanyService : ICompanyService
    {
        protected readonly IAccountRepository accountRepository;
        protected readonly IAgentRepository agentRepository;
        protected readonly IAgentAssignmentRepository agentAssignmentRepository;
        protected readonly ICompanyRepository companyRepository;
        protected readonly ICompanyTypeModificationRepository companyTypeModificationRepository;
        protected readonly IRoleRepository roleRepository;

        protected readonly IVerificationProcessService verificationProcessService;

        protected readonly IUnitOfWork unitOfWork;
        protected readonly IEmailHelper emailHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompanyService(
            IAccountRepository accountRepository,
            IAgentRepository agentRepository,
            IAgentAssignmentRepository agentAssignmentRepository,
            ICompanyRepository companyRepository,
            ICompanyTypeModificationRepository companyTypeModificationRepository,
            IRoleRepository roleRepository,

            IVerificationProcessService verificationProcessService,

            IUnitOfWork unitOfWork,
            IEmailHelper emailHelper,
            IHttpContextAccessor httpContextAccessor
            )
        {
            this.accountRepository = accountRepository;
            this.agentRepository = agentRepository;
            this.agentAssignmentRepository = agentAssignmentRepository;
            this.companyRepository = companyRepository;
            this.companyTypeModificationRepository = companyTypeModificationRepository;
            this.roleRepository = roleRepository;

            this.verificationProcessService = verificationProcessService;

            this.unitOfWork = unitOfWork;
            this.emailHelper = emailHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        public ICollection<Company> GetAll()
        {
            var role = (Role)_httpContextAccessor.HttpContext.Items["Role"];
            if (role == null)
            {
                return Array.Empty<Company>();
            }
            if (role.RoleName == "Admin")
            {
                return companyRepository.GetAll().ToList();
            }
            var account = (Account)_httpContextAccessor.HttpContext.Items["Account"];
            var agent = agentRepository.GetByAccountId(account.Id);
            var assigneds = agentAssignmentRepository.GetByAgentId(agent.Id);
            var provinceIds = assigneds.Select(x => x.ProvinceId).ToList();
            return companyRepository.GetByProvinces(provinceIds);
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

        public ICollection<CompanyTypeModification> GetModificationReportPrivate(int month, int year)
        {
            return companyTypeModificationRepository.GetModificationReportPrivate(month, year);
        }

        public ICollection<CompanyTypeModification> GetCompanyModificationReport(int companyId)
        {
            return companyTypeModificationRepository.GetCompanyModificationReport(companyId);
        }

        public CompanyTypeModification GetModificationById(int id)
        {
            return companyTypeModificationRepository.GetById(id);
        }

        public async Task<Company> RegisterCompany(CompanyRegistrationDTO data)
        {
            // validate
            var existedAccount = accountRepository.GetByEmail(data.Email);
            if (existedAccount != null)
            {
                throw new BadHttpRequestException("EmailAlreadyExisted");
            }

            var existedCompany = companyRepository.GetByCompanyCode(data.CompanyCode);
            if (existedCompany != null)
            {
                throw new BadHttpRequestException("CompanyCodeAlreadyExisted");
            }

            var role = roleRepository.GetRoleByName("Company");
            if (role == null)
            {
                throw new Exception("Lỗi: không tồn tại role trong hệ thống");
            }

            using var transaction = unitOfWork.BeginTransaction();
            var rawPassword = "abcd1234";
            //var rawPassword = Guid.NewGuid().ToString();
            var account = new Account
            {
                Email = data.Email,
                Password = CommonUtils.GenerateSHA1(rawPassword),
                IsVerified = true,
                RoleId = role.Id,
            };
            await accountRepository.AddAsync(account);

            var company = new Company
            {
                CompanyCode = data.CompanyCode,
                CompanyNameEN = data.CompanyNameEN,
                CompanyNameVI = data.CompanyNameVI,
                AccountId = account.Id,
                ProvinceId = data.ProvinceId
            };
            await companyRepository.AddAsync(company);

            var mailParams = new Dictionary<string, string>
            {
                { "companyName", company.CompanyNameVI },
                { "email", account.Email },
                { "password", rawPassword }
            };

            try
            {
                await emailHelper.SendEmailAsync(
                    new string[] { account.Email },
                    "Thông tin tài khoản đăng nhập",
                    EmailTemplate.CompanyRegistrationVerified,
                    mailParams);
            }
            catch (Exception)
            {

            }
            transaction.Commit();

            await verificationProcessService.GenerateAsync(company.Id);

            return company;
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

            await verificationProcessService.GenerateAsync(company.Id);

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

        public async Task<CompanyTypeModification> UpdateModificationAsync(CompanyTypeModification payload)
        {
            var modification = companyTypeModificationRepository.GetById(payload.Id);
            if (modification == null)
            {
                throw new BadHttpRequestException("CompanyTypeModificationNotExist");
            }
            modification.IsAnnounced = payload.IsAnnounced;
            modification.PreviousCompanyTypeId = payload.PreviousCompanyTypeId;
            modification.UpdatedCompanyTypeId = payload.UpdatedCompanyTypeId;
            return await companyTypeModificationRepository.UpdateAsync(modification);
        }

    }
}
