using EcisApi.Models;
using EcisApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Services
{
    public interface ICriteriaService
    {
        ICollection<Criteria> GetAll();
        Criteria GetById(int id);
    }

    public class CriteriaService : ICriteriaService
    {
        protected readonly ICriteriaRepository criteriaRepository;

        public CriteriaService(
            ICriteriaRepository criteriaRepository
            )
        {
            this.criteriaRepository = criteriaRepository;
        }

        public ICollection<Criteria> GetAll()
        {
            return criteriaRepository.GetAll().ToList();
        }

        public Criteria GetById(int id)
        {
            return criteriaRepository.GetById(id);
        }
    }
}
