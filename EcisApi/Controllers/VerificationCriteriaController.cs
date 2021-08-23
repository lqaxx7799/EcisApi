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
    public class VerificationCriteriaController : ControllerBase
    {
        private readonly IVerificationCriteriaService verificationCriteriaService;

        public VerificationCriteriaController(IVerificationCriteriaService verificationCriteriaService)
        {
            this.verificationCriteriaService = verificationCriteriaService;
        }

        [HttpGet("GetByProcessId/{processId}")]
        [Authorize]
        public ActionResult<ICollection<VerificationCriteria>> GetByProcessId([FromRoute] int processId)
        {
            return Ok(verificationCriteriaService.GetByProcessId(processId));
        }
    }
}
