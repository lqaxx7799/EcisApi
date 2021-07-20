using EcisApi.DTO;
using EcisApi.Helpers;
using EcisApi.Models;
using EcisApi.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Services
{
    public interface IAccountService
    {
        Account GetById(int id);
        AuthenticateResponseDTO Authenticate(AuthenticateRequestDTO model);
    }

    public class AccountService : IAccountService
    {
        protected readonly IAccountRepository accountRepository;
        protected readonly AppSettings appSettings;

        public AccountService(
            IAccountRepository accountRepository,
            IOptions<AppSettings> appSettings
            )
        {
            this.accountRepository = accountRepository;
            this.appSettings = appSettings.Value;
        }

        public Account GetById(int id)
        {
            return accountRepository.GetById(id);
        }

        public AuthenticateResponseDTO Authenticate(AuthenticateRequestDTO model)
        {
            var hashedPassword = CommonUtils.GenerateSHA1(model.Password);
            var account = accountRepository.GetOne(x => x.Email == model.Email && x.Password == hashedPassword);
            
            if (account == null) return null;

            var token = CommonUtils.GenerateJwtToken(account, appSettings.Secret);
            return new AuthenticateResponseDTO
            {
                Id = account.Id,
                Email = account.Email,
                IsVerified = account.IsVerified,
                RoleId = account.RoleId,
                Token = token
            };
                
        }
    }
}
