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
        ThirdParty GetByAccountId(int accountId);
        Task<ThirdParty> AddAsync(ThirdPartyRegisterDTO payload);
        Task<ThirdParty> ResetClientSecretAsync(int id);
        Task<ThirdParty> ActivateAsync(int id);
        Task<ThirdParty> DeactivateAsync(int id);
    }

    public class ThirdPartyService : IThirdPartyService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IThirdPartyRepository thirdPartyRepository;

        private readonly IUnitOfWork unitOfWork;
        private readonly IEmailHelper emailHelper;

        public ThirdPartyService(
            IAccountRepository accountRepository,
            IRoleRepository roleRepository,
            IThirdPartyRepository thirdPartyRepository,
            IUnitOfWork unitOfWork,
            IEmailHelper emailHelper)
        {
            this.accountRepository = accountRepository;
            this.roleRepository = roleRepository;
            this.thirdPartyRepository = thirdPartyRepository;
            this.unitOfWork = unitOfWork;

            this.emailHelper = emailHelper;
        }

        public ICollection<ThirdParty> GetAll()
        {
            return thirdPartyRepository.GetAll();
        }

        public ThirdParty GetById(int id)
        {
            return thirdPartyRepository.GetById(id);
        }

        public ThirdParty GetByAccountId(int accountId)
        {
            return thirdPartyRepository.GetByAccountId(accountId);
        }

        public async Task<ThirdParty> AddAsync(ThirdPartyRegisterDTO payload)
        {
            var existingAccount = accountRepository.GetByEmail(payload.Email);
            if (existingAccount != null)
            {
                throw new BadHttpRequestException("EmailExisted");
            }
            var role = roleRepository.GetRoleByName("ThirdParty");
            if (role == null)
            {
                throw new BadHttpRequestException("RoleNotFound");
            }

            using var transaction = unitOfWork.BeginTransaction();
            var password = "abcd1234";
            Account account = new()
            {
                Email = payload.Email,
                Password = CommonUtils.GenerateSHA1(password),
                IsVerified = true,
                RoleId = role.Id,
                IsDeleted = false
            };
            await accountRepository.AddAsync(account);
            ThirdParty thirdParty = new()
            {
                UserName = payload.UserName,
                ClientId = Guid.NewGuid().ToString(),
                ClientSecret = CommonUtils.GenerateRandomHexString(32),
                IsActive = true,
                IsDeleted = false,
                AccountId = account.Id
            };
            await thirdPartyRepository.AddAsync(thirdParty);
            transaction.Commit();

            var mailParams = new Dictionary<string, string>
            {
                { "userName", thirdParty.UserName },
                { "email", account.Email },
                { "password", password }
            };
            try
            {
                await emailHelper.SendEmailAsync(
                    new string[] { payload.Email },
                    "Thông tin tài khoản đăng nhập",
                    EmailTemplate.AgentCreated,
                    mailParams);
            }
            catch (Exception)
            {

            }
            return thirdParty;
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
            if (thirdParty.IsDeleted || !thirdParty.Account.IsVerified || thirdParty.Account.IsDeleted)
            {
                throw new BadHttpRequestException("ThirdPartyInvalid");
            }
            if (thirdParty.IsActive)
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
            if (thirdParty.IsDeleted || !thirdParty.Account.IsVerified || thirdParty.Account.IsDeleted)
            {
                throw new BadHttpRequestException("ThirdPartyInvalid");
            }
            if (!thirdParty.IsActive)
            {
                throw new BadHttpRequestException("ThirdPartyInvalid");
            }
            thirdParty.IsActive = false;
            return await thirdPartyRepository.UpdateAsync(thirdParty);
        }
    }
}
