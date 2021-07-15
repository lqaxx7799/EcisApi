using EcisApi.Models;
using EcisApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Services
{
    public interface IAccountService
    {
        Account GetById(int id);
    }

    public class AccountService : IAccountService
    {
        protected readonly IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public Account GetById(int id)
        {
            return accountRepository.GetById(id);
        }
    }
}
