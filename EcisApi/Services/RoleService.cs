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
    public interface IRoleService
    {
        Role GetById(int id);
    }

    public class RoleService : IRoleService
    {
        protected readonly IRoleRepository roleRepository;

        public RoleService(
            IRoleRepository roleRepository
            )
        {
            this.roleRepository = roleRepository;
        }

        public Role GetById(int id)
        {
            return roleRepository.GetById(id);
        }
    }
}
