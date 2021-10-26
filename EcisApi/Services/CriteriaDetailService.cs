using EcisApi.Models;
using EcisApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Services
{
    public interface ICriteriaDetailService
    {
        ICollection<CriteriaDetail> GetAll();
        CriteriaDetail GetById(int id);
    }

    public class CriteriaDetailService : ICriteriaDetailService
    {
        protected readonly ICriteriaDetailRepository criteriaDetailRepository;

        public CriteriaDetailService(
            ICriteriaDetailRepository criteriaDetailRepository
            )
        {
            this.criteriaDetailRepository = criteriaDetailRepository;
        }

        public ICollection<CriteriaDetail> GetAll()
        {
            return criteriaDetailRepository.GetAll().ToList();
        }

        public CriteriaDetail GetById(int id)
        {
            return criteriaDetailRepository.GetById(id);
        }
    }
}
