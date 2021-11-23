using EcisApi.Models;
using EcisApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Services
{
    public interface IProvinceService
    {
        ICollection<Province> GetAll();
    }

    public class ProvinceService : IProvinceService
    {
        protected readonly IProvinceRepository provinceRepository;

        public ProvinceService(IProvinceRepository provinceRepository)
        {
            this.provinceRepository = provinceRepository;
        }

        public ICollection<Province> GetAll()
        {
            return provinceRepository.GetAll().ToList();
        }
    }
}
