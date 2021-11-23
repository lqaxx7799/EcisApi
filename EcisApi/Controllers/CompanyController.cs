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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService companyService;

        public CompanyController(
            ICompanyService companyService
            )
        {
            this.companyService = companyService;
        }

        [HttpGet("GetAll")]
        [Authorize("Agent", "Admin")]
        public ActionResult<ICollection<Company>> GetAll()
        {
            return Ok(companyService.GetAll());
        }

        [HttpGet("GetAllActivated")]
        [Authorize]
        public ActionResult<ICollection<Company>> GetAllActivated()
        {
            return Ok(companyService.GetAllActivated());
        }

        [HttpGet("ByAccount/{accountId}")]
        public ActionResult<Company> GetByCompanyId([FromRoute] int accountId)
        {
            return companyService.GetByAccountId(accountId);
        }

        [HttpGet("GetReportPrivate")]
        [Authorize("Admin", "Agent")]
        public ActionResult<ICollection<CompanyTypeModification>> GetModificationReportPrivate([FromQuery] int? month, [FromQuery] int? year)
        {
            var result = companyService.GetModificationReportPrivate(month.GetValueOrDefault(DateTime.Now.Month), year.GetValueOrDefault(DateTime.Now.Year));
            return Ok(result);
        }

        [HttpGet("GetReport")]
        //[Authorize]
        public ActionResult<ICollection<CompanyTypeModification>> GetModificationReport([FromQuery] int? month, [FromQuery] int? year)
        {
            var result = companyService.GetModificationReport(month.GetValueOrDefault(DateTime.Now.Month), year.GetValueOrDefault(DateTime.Now.Year));
            return Ok(result);
        }

        [HttpGet("GetCompanyReport/{id}")]
        [Authorize]
        public ActionResult<ICollection<CompanyTypeModification>> GetCompanyModificationReport([FromRoute] int id)
        {
            return Ok(companyService.GetCompanyModificationReport(id));
        }

        [HttpGet("Modification/ById/{id}")]
        [Authorize]
        public ActionResult<CompanyTypeModification> GetModificationById([FromRoute] int id)
        {
            return Ok(companyService.GetModificationById(id));
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Company> GetById([FromRoute] int id)
        {
            return companyService.GetById(id);
        }

        [HttpPost("RegisterCompany")]
        [Authorize("Admin")]
        public async Task<ActionResult<Company>> RegisterCompany([FromBody] CompanyRegistrationDTO payload)
        {
            try
            {
                return await companyService.RegisterCompany(payload);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    e.Message,
                });
            }
        }

        [HttpPost("VerifyCompany")]
        [Authorize]
        public async Task<ActionResult<Account>> VerifyCompany([FromBody] VerifyCompanyDTO payload)
        {
            try
            {
                return await companyService.VerifyCompany(payload.AccountId);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new
                {
                    e.Message,
                });
            }
        }

        [HttpPut("UpdateModification")]
        [Authorize]
        public async Task<ActionResult<CompanyTypeModification>> UpdateModification([FromBody] CompanyTypeModification payload)
        {
            try
            {
                return await companyService.UpdateModificationAsync(payload);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new
                {
                    e.Message,
                });
            }
        }

        //public async Task<ActionResult<CompanyTypeModification>> ModifyType([FromBody] ModifyCompanyTypeDTO payload)
        //{
        //    return await companyService.ModifyType(payload);
        //}
    }
}
