using EcisApi.DTO;
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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService companyService;

        public CompanyController(
            ICompanyService companyService
            )
        {
            this.companyService = companyService;
        }

        [HttpPost]
        public ActionResult<string> RegisterCompany([FromBody] CompanyRegistrationDTO payload)
        {
            companyService.RegisterCompany(payload);
            return "OK";
        }
    }
}
