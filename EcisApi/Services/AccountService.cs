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

    }

    public class AccountService : IAccountService
    {
        protected readonly AccountRepository accountRepository;

        public AccountService(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public Account FindById(int id)
        {
            return accountRepository.FindById(id);
        }
    }
}
