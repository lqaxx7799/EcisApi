using EcisApi.Helpers;
using EcisApi.Models;
using EcisApi.Services;
using Microsoft.AspNetCore.Http;
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

        [HttpPut("Update")]
        [Authorize]
        public async Task<ActionResult<VerificationCriteria>> Update([FromBody] VerificationCriteria payload)
        {
            try
            {
                var updated = await verificationCriteriaService.UpdateAsync(payload);
                return Ok(updated);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpPut("ApproveAll/{processId}")]
        [Authorize("Agent", "Admin")]
        public async Task<ActionResult<ICollection<VerificationCriteria>>> ApproveAll([FromRoute] int processId)
        {
            try
            {
                var updated = await verificationCriteriaService.ApproveAllAsync(processId);
                return Ok(updated);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }
    }
}
