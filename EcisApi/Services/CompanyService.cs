using EcisApi.DTO;
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
        Task RegisterCompany(CompanyRegistrationDTO data);
    }

    public class CompanyService : ICompanyService
    {
        protected readonly IAccountRepository accountRepository;
        protected readonly ICompanyRepository companyRepository;
        protected readonly IRoleRepository roleRepository;

        public CompanyService(
            IAccountRepository accountRepository,
            ICompanyRepository companyRepository,
            IRoleRepository roleRepository
            )
        {
            this.accountRepository = accountRepository;
            this.companyRepository = companyRepository;
            this.roleRepository = roleRepository;
        }

        public Company GetById(int id)
        {
            return companyRepository.GetById(id);
        }

        public async Task RegisterCompany(CompanyRegistrationDTO data)
        {
            // validate
            var role = roleRepository.GetRoleByName("COMPANY");
            if (role == null)
            {
                throw new Exception("Role company does not exist");
            }

            var account = new Account
            {
                Email = data.Email,
                Password = Guid.NewGuid().ToString(),
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
        }
    }
}
