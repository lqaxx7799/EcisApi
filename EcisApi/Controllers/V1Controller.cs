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
    public class V1Controller : ControllerBase
    {
        private readonly IV1Service v1Service;

        public V1Controller(IV1Service v1Service)
        {
            this.v1Service = v1Service;
        }

        [HttpGet("Account/{id}")]
        [V1Authorize]
        public ActionResult<ThirdParty> GetThirdPartyById([FromRoute] int id)
        {
            try
            {
                var result = v1Service.GetById(id);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("Companies")]
        [V1Authorize]
        public ActionResult<ICollection<PublicCompanyDTO>> GetCompanies()
        {
            try
            {
                var result = v1Service.GetCompanies();
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("Company/ById/{id}")]
        [V1Authorize]
        public ActionResult<PublicCompanyDTO> GetCompanyById([FromRoute] int id)
        {
            try
            {
                var result = v1Service.GetCompanyById(id);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("Company/ByCode/{id}")]
        [V1Authorize]
        public ActionResult<PublicCompanyDTO> GetCompanyByCode([FromRoute] string code)
        {
            try
            {
                var result = v1Service.GetCompanyByCode(code);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("Report/ByTime")]
        [V1Authorize]
        public ActionResult<ICollection<PublicCompanyDTO>> GetModificationReport([FromQuery] int? month, [FromQuery] int? year)
        {
            try
            {
                var result = v1Service.GetModificationReport(month.GetValueOrDefault(DateTime.Now.Month), year.GetValueOrDefault(DateTime.Now.Year));
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("Report/ByCompanyId/{id}")]
        [V1Authorize]
        public ActionResult<ICollection<PublicCompanyDTO>> GetModificationReportByCompanyId([FromRoute] int id)
        {
            try
            {
                var result = v1Service.GetModificationReportByCompanyId(id);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpPost("Authenticate")]
        public ActionResult<PublicV1AuthenticateResponseDTO> Authenticate([FromBody] PublicV1AuthenticateDTO payload)
        {
            try
            {
                var result = v1Service.Authenticate(payload);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return Unauthorized(new { e.Message });
            }
        }
    }
}
