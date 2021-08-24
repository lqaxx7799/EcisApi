using EcisApi.Helpers;
using EcisApi.Models;
using EcisApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriteriaTypeController : ControllerBase
    {
        private readonly ICriteriaTypeService criteriaTypeService;

        public CriteriaTypeController(ICriteriaTypeService criteriaTypeService)
        {
            this.criteriaTypeService = criteriaTypeService;
        }

        [HttpGet("")]
        [Authorize]
        public ActionResult<ICollection<CriteriaType>> GetAll()
        {
            return Ok(criteriaTypeService.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<CriteriaType> GetById([FromRoute] int id)
        {
            return Ok(criteriaTypeService.GetById(id));
        }
    }
}
