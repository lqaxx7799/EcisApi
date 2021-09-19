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

        [HttpGet("ByAccount/{accountId}")]
        public ActionResult<Company> GetByCompanyId([FromRoute] int accountId)
        {
            return companyService.GetByAccountId(accountId);
        }

        [HttpPost("RegisterCompany")]
        public async Task<ActionResult<dynamic>> RegisterCompany([FromBody] CompanyRegistrationDTO payload)
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

        public async Task<ActionResult<CompanyTypeModification>> ModifyType([FromBody] ModifyCompanyTypeDTO payload)
        {
            return await companyService.ModifyType(payload);
        }
    }
}
