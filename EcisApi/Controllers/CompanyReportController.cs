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
    public class CompanyReportController : ControllerBase
    {
        private readonly ICompanyReportService companyReportService;

        public CompanyReportController(ICompanyReportService companyReportService)
        {
            this.companyReportService = companyReportService;
        }

        [HttpGet("GetAll")]
        [Authorize]
        public ActionResult<ICollection<CompanyReport>> GetAll()
        {
            return Ok(companyReportService.GetAll());
        }

        [HttpGet("CanCreateReport/{id}")]
        [Authorize]
        public ActionResult<bool> CanCreateReport([FromRoute] int id)
        {
            return Ok(companyReportService.CanCreateReport(id));
        }


        [HttpGet("ById/{id}")]
        [Authorize]
        public ActionResult<CompanyReport> GetById([FromRoute] int id)
        {
            return Ok(companyReportService.GetById(id));
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<ActionResult<CompanyReport>> Create([FromBody] CompanyReportDTO payload)
        {
            try
            {
                var result = await companyReportService.AddAsync(payload);
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
        public async Task<ActionResult<CompanyReport>> Approve([FromRoute] int id)
        {
            try
            {
                var result = await companyReportService.ApproveAsync(id);
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
        public async Task<ActionResult<CompanyReport>> Reject([FromRoute] int id)
        {
            try
            {
                var result = await companyReportService.RejectAsync(id);
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
