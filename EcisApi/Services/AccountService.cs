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
    public interface IAccountService
    {
        Account GetById(int id);
        AuthenticateResponseDTO Authenticate(AuthenticateRequestDTO model);
        AuthenticateResponseDTO AuthenticateManagement(AuthenticateRequestDTO model);
        Task ChangePassword(Account account, ChangePasswordDTO payload);
    }

    public class AccountService : IAccountService
    {
        protected readonly IAccountRepository accountRepository;
        protected readonly IRoleRepository roleRepository;
        protected readonly AppSettings appSettings;

        public AccountService(
            IAccountRepository accountRepository,
            IRoleRepository roleRepository,
            IOptions<AppSettings> appSettings
            )
        {
            this.accountRepository = accountRepository;
            this.roleRepository = roleRepository;
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
            
            if (account == null || !account.IsVerified) return null;

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

        public AuthenticateResponseDTO AuthenticateManagement(AuthenticateRequestDTO model)
        {
            var hashedPassword = CommonUtils.GenerateSHA1(model.Password);
            var account = accountRepository.GetOne(x => x.Email == model.Email && x.Password == hashedPassword);

            if (account == null)
            {
                throw new BadHttpRequestException("Sai email hoặc mật khẩu");
            }

            if (!account.IsVerified || !account.Role.HasManagement)
            {
                throw new BadHttpRequestException("Tài khoản không có quyền đăng nhập");
            }

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

        public async Task ChangePassword(Account account, ChangePasswordDTO payload)
        {
            if (account.Password != CommonUtils.GenerateSHA1(payload.OldPassword))
            {
                throw new BadHttpRequestException("WrongOldPassword");
            }
            account.Password = CommonUtils.GenerateSHA1(payload.NewPassword);
            await accountRepository.UpdateAsync(account);
        }
    }
}
