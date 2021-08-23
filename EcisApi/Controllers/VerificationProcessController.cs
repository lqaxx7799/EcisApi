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
    public class VerificationProcessController : ControllerBase
    {
        private readonly IVerificationProcessService verificationProcessService;

        public VerificationProcessController(IVerificationProcessService verificationProcessService)
        {
            this.verificationProcessService = verificationProcessService;
        }

        [HttpGet("GetByCompany/{companyId}")]
        [Authorize("Company")]
        public ActionResult<ICollection<VerificationProcess>> GetByCompany([FromRoute] int companyId)
        {
            return Ok(verificationProcessService.GetByCompany(companyId));
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<VerificationProcess> GetById([FromRoute] int id)
        {
            return Ok(verificationProcessService.GetById(id));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<VerificationProcess>> Add([FromBody] VerificationProcess payload)
        {
            return await verificationProcessService.AddAsync(payload);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<VerificationProcess>> Update([FromBody] VerificationProcess payload)
        {
            return await verificationProcessService.UpdateAsync(payload);
        }
    }
}
