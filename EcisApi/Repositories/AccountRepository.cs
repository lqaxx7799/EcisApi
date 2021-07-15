﻿using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface IAccountRepository: IRepository<Account>
    {

    }


    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(DataContext dataContext) : base(dataContext)
        {

        }

    }
}
