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
    public class ViolationReportController : ControllerBase
    {
        private readonly IViolationReportService violationReportService;

        public ViolationReportController(IViolationReportService violationReportService)
        {
            this.violationReportService = violationReportService;
        }

        [HttpGet("GetAll")]
        [Authorize]
        public ActionResult<ICollection<ViolationReport>> GetAll()
        {
            return Ok(violationReportService.GetAll());
        }

        [HttpGet("ById/{id}")]
        [Authorize]
        public ActionResult<ViolationReport> GetById([FromRoute] int id)
        {
            return Ok(violationReportService.GetById(id));
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<ActionResult<ViolationReport>> Create([FromBody] ViolationReportDTO payload)
        {
            try
            {
                var result = await violationReportService.AddAsync(payload);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new
                {
                    e.Message,
                });
            }
        }

        [HttpPut("Approve/{id}")]
        [Authorize]
        public async Task<ActionResult<ViolationReport>> Approve([FromRoute] int id)
        {
            try
            {
                var result = await violationReportService.ApproveAsync(id);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new
                {
                    e.Message,
                });
            }
        }

        [HttpPut("Reject/{id}")]
        [Authorize]
        public async Task<ActionResult<ViolationReport>> Reject([FromRoute] int id)
        {
            try
            {
                var result = await violationReportService.RejectAsync(id);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new
                {
                    e.Message,
                });
            }
        }
    }
}
