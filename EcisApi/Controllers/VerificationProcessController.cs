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
    public class VerificationProcessController : ControllerBase
    {
        private readonly IVerificationProcessService verificationProcessService;

        public VerificationProcessController(IVerificationProcessService verificationProcessService)
        {
            this.verificationProcessService = verificationProcessService;
        }

        [HttpGet("GetAll")]
        [Authorize("Agent", "Admin")]
        public ActionResult<ICollection<VerificationProcess>> GetAll()
        {
            return Ok(verificationProcessService.GetAll());
        }

        [HttpGet("GetPending")]
        [Authorize("Agent", "Admin")]
        public ActionResult<ICollection<VerificationProcess>> GetAllPending()
        {
            return Ok(verificationProcessService.GetAllPending());
        }

        [HttpGet("GetSupport")]
        [Authorize("Agent", "Admin")]
        public ActionResult<ICollection<VerificationProcess>> GetAllSupport()
        {
            return Ok(verificationProcessService.GetAllSupport());
        }

        [HttpGet("GetReviewed")]
        [Authorize("Agent", "Admin")]
        public ActionResult<ICollection<VerificationProcess>> GetAllReviewed()
        {
            return Ok(verificationProcessService.GetAllReviewed());
        }

        [HttpGet("GetByCompany/{companyId}")]
        [Authorize("Company", "Agent", "Admin")]
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

        [HttpGet("GetCurrentPending/{companyId}")]
        [Authorize("Company")]
        public ActionResult<VerificationProcess> GetCompanyCurrentPending([FromRoute] int companyId)
        {
            return Ok(verificationProcessService.GetCompanyCurrentPending(companyId));
        }

        [HttpGet("GetLast/{companyId}")]
        [Authorize("Company")]
        public ActionResult<VerificationProcess> GetCompanyLast([FromRoute] int companyId)
        {
            return Ok(verificationProcessService.GetCompanyLast(companyId));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<VerificationProcess>> Add([FromBody] VerificationProcess payload)
        {
            return await verificationProcessService.AddAsync(payload);
        }

        [HttpPost("Generate/{companyId}")]
        public async Task<ActionResult<VerificationProcess>> Generate([FromRoute] int companyId)
        {
            return await verificationProcessService.GenerateAsync(companyId);
        }

        [HttpPut("Support/{id}")]
        [Authorize("Company")]
        public async Task<ActionResult<VerificationProcess>> RequestSupport([FromRoute] int id)
        {
            try
            {
                return await verificationProcessService.RequestSupportAsync(id);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpPut("SubmitProcess/{id}")]
        [Authorize("Company")]
        public async Task<ActionResult<VerificationProcess>> SubmitProcess([FromRoute] int id)
        {
            try
            {
                return await verificationProcessService.SubmitProcessAsync(id);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpPut("SubmitReview/{id}")]
        [Authorize("Admin", "Agent")]
        public async Task<ActionResult<VerificationProcess>> SubmitReview([FromRoute] int id)
        {
            try
            {
                return await verificationProcessService.SubmitReviewAsync(id);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpPut("Finish/{id}")]
        [Authorize("Agent", "Admin")]
        public async Task<ActionResult<VerificationProcess>> Finish([FromRoute] int id)
        {
            try
            {
                return await verificationProcessService.FinishAsync(id);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<VerificationProcess>> Update([FromBody] VerificationProcess payload)
        {
            return await verificationProcessService.UpdateAsync(payload);
        }
    }
}
