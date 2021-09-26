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
    public interface ICompanyTypeService
    {
        ICollection<CompanyType> GetAll();
        CompanyType GetById(int id);
    }

    public class CompanyTypeService : ICompanyTypeService
    {
        protected readonly ICompanyTypeRepository companyTypeRepository;

        public CompanyTypeService(
            ICompanyTypeRepository companyTypeRepository
            )
        {
            this.companyTypeRepository = companyTypeRepository;
        }

        public ICollection<CompanyType> GetAll()
        {
            return companyTypeRepository.GetAll().ToList();
        }

        public CompanyType GetById(int id)
        {
            return companyTypeRepository.GetById(id);
        }
    }
}
