﻿using EcisApi.DTO;
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

        [HttpPost("RegisterCompany")]
        public async Task<ActionResult<dynamic>> RegisterCompany([FromBody] CompanyRegistrationDTO payload)
        {
            return await companyService.RegisterCompany(payload);
        }

        public async Task<ActionResult<CompanyTypeModification>> ModifyType([FromBody] ModifyCompanyTypeDTO payload)
        {
            return await companyService.ModifyType(payload);
        }
    }
}
