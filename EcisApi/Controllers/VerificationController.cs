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
    public class VerificationController : ControllerBase
    {
        private readonly IVerificationService verificationService;

        public VerificationController(IVerificationService verificationService)
        {
            this.verificationService = verificationService;
        }

        [HttpGet("Process/{companyId}")]
        public ActionResult<ICollection<VerificationProcess>> GetByCompanyId([FromRoute] int companyId)
        {
            return Ok(verificationService.GetByCompany(companyId));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<VerificationProcess>> Add([FromBody] VerificationProcess payload)
        {
            return await verificationService.AddAsync(payload);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<VerificationProcess>> Update([FromBody] VerificationProcess payload)
        {
            return await verificationService.UpdateAsync(payload);
        }
    }
}
