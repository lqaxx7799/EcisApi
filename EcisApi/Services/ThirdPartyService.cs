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
    public interface IThirdPartyService
    {
        ICollection<ThirdParty> GetAll();
        ThirdParty GetById(int id);
        Task<ThirdParty> AddAsync(ThirdPartyRegisterDTO payload);
        Task<ThirdParty> ResetClientSecretAsync(int id);
        Task<ThirdParty> ActivateAsync(int id);
        Task<ThirdParty> DeactivateAsync(int id);
    }

    public class ThirdPartyService : IThirdPartyService
    {
        private readonly IAccountRepository accountRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly ICompanyTypeModificationRepository companyTypeModificationRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IThirdPartyRepository thirdPartyRepository;

        public ThirdPartyService(
            IAccountRepository accountRepository,
            ICompanyRepository companyRepository,
            ICompanyTypeModificationRepository companyTypeModificationRepository,
            IRoleRepository roleRepository,
            IThirdPartyRepository thirdPartyRepository)
        {
            this.accountRepository = accountRepository;
            this.companyRepository = companyRepository;
            this.companyTypeModificationRepository = companyTypeModificationRepository;
            this.roleRepository = roleRepository;
            this.thirdPartyRepository = thirdPartyRepository;
        }

        public ICollection<ThirdParty> GetAll()
        {
            return thirdPartyRepository.GetAll();
        }

        public ThirdParty GetById(int id)
        {
            return thirdPartyRepository.GetById(id);
        }

        public async Task<ThirdParty> AddAsync(ThirdPartyRegisterDTO payload)
        {
            var existingAccount = accountRepository.GetByEmail(payload.Email);
            if (existingAccount != null)
            {
                throw new BadHttpRequestException("EmailExisted");
            }
            var role = roleRepository.GetRoleByName("Third Party");
            if (role == null)
            {
                throw new BadHttpRequestException("RoleNotFound");
            }
            Account account = new()
            {
                Email = payload.Email,
                Password = CommonUtils.GenerateSHA1(payload.Password),
                IsVerified = false,
                RoleId = role.Id,
                IsDeleted = false
            };
            await accountRepository.AddAsync(account);
            ThirdParty thirdParty = new()
            {
                UserName = payload.UserName,
                ClientId = Guid.NewGuid().ToString(),
                ClientSecret = CommonUtils.GenerateRandomHexString(32),
                IsActive = false,
                IsDeleted = false,
                AccountId = account.Id
            };
            return await thirdPartyRepository.AddAsync(thirdParty);
        }

        public async Task<ThirdParty> ResetClientSecretAsync(int id)
        {
            var thirdParty = thirdPartyRepository.GetById(id);
            if (thirdParty == null)
            {
                throw new BadHttpRequestException("ThirdPartyNotExisted");
            }
            if (!thirdParty.IsActive || thirdParty.IsDeleted || !thirdParty.Account.IsVerified || thirdParty.Account.IsDeleted)
            {
                throw new BadHttpRequestException("ThirdPartyInvalid");
            }
            thirdParty.ClientSecret = CommonUtils.GenerateRandomHexString(32);
            return await thirdPartyRepository.UpdateAsync(thirdParty);
        }

        public async Task<ThirdParty> ActivateAsync(int id)
        {
            var thirdParty = thirdPartyRepository.GetById(id);
            if (thirdParty == null)
            {
                throw new BadHttpRequestException("ThirdPartyNotExisted");
            }
            if (!thirdParty.IsActive || thirdParty.IsDeleted || !thirdParty.Account.IsVerified || thirdParty.Account.IsDeleted)
            {
                throw new BadHttpRequestException("ThirdPartyInvalid");
            }
            thirdParty.IsActive = true;
            await thirdPartyRepository.UpdateAsync(thirdParty);

            thirdParty.Account.IsVerified = true;
            await accountRepository.UpdateAsync(thirdParty.Account);
            return thirdParty;
        }

        public async Task<ThirdParty> DeactivateAsync(int id)
        {
            var thirdParty = thirdPartyRepository.GetById(id);
            if (thirdParty == null)
            {
                throw new BadHttpRequestException("ThirdPartyNotExisted");
            }
            if (!thirdParty.IsActive || thirdParty.IsDeleted || !thirdParty.Account.IsVerified || thirdParty.Account.IsDeleted)
            {
                throw new BadHttpRequestException("ThirdPartyInvalid");
            }
            thirdParty.IsActive = false;
            return await thirdPartyRepository.UpdateAsync(thirdParty);
        }
    }
}
