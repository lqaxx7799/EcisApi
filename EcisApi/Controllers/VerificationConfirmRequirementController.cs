using EcisApi.DTO;
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
    public class VerificationConfirmRequirementController : ControllerBase
    {
        private readonly IVerificationConfirmRequirementService verificationConfirmRequirementService;

        public VerificationConfirmRequirementController(
            IVerificationConfirmRequirementService verificationConfirmRequirementService
            )
        {
            this.verificationConfirmRequirementService = verificationConfirmRequirementService;
        }

        [HttpGet("AssignedPending/{agentId}")]
        [Authorize]
        public ActionResult<ICollection<VerificationConfirmRequirement>> GetAssignedPending([FromRoute] int agentId)
        {
            return Ok(verificationConfirmRequirementService.GetPendingByAgentId(agentId));
        }

        [HttpGet("AssignedFinished/{agentId}")]
        [Authorize]
        public ActionResult<ICollection<VerificationConfirmRequirement>> GetAssignedFinished([FromRoute] int agentId)
        {
            return Ok(verificationConfirmRequirementService.GetFinishedByAgentId(agentId));
        }

        [HttpGet("PendingByCompanyId/{companyId}")]
        [Authorize]
        public ActionResult<ICollection<VerificationConfirmRequirement>> GetPendingByCompanyId([FromRoute] int companyId)
        {
            return Ok(verificationConfirmRequirementService.GetPendingByCompanyId(companyId));
        }

        [HttpGet("ByProcessId/{processId}")]
        [Authorize]
        public ActionResult<VerificationConfirmRequirement> GetOneByProcessId([FromRoute] int processId)
        {
            return Ok(verificationConfirmRequirementService.GetOneByProcessId(processId));
        }

        [HttpGet("ById/{id}")]
        [Authorize]
        public ActionResult<VerificationConfirmRequirement> GetById([FromRoute] int id)
        {
            return Ok(verificationConfirmRequirementService.GetById(id));
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<ActionResult<VerificationConfirmRequirement>> Create([FromBody] VerificationConfirmRequirement payload)
        {
            var result = await verificationConfirmRequirementService.AddAsync(payload);
            return Ok(result);
        }

        //[HttpPut("AnnounceCompany")]
        //[Authorize]
        //public async Task<ActionResult<VerificationConfirmRequirement>> AnnounceCompany([FromBody] VerificationConfirmUpdateDTO payload)
        //{
        //    try
        //    {
        //        var result = await verificationConfirmRequirementService.AnnounceCompanyAsync(payload);
        //        return Ok(result);
        //    }
        //    catch (BadHttpRequestException e)
        //    {
        //        return BadRequest(new { e.Message });
        //    }
        //}

        [HttpPut("FinishConfirm")]
        [Authorize]
        public async Task<ActionResult<VerificationConfirmRequirement>> FinishConfirm([FromBody] VerificationConfirmUpdateDTO payload)
        {
            try
            {
                var result = await verificationConfirmRequirementService.FinishConfirmAsync(payload);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }
    }
}
