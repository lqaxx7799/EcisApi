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
    public class ThirdPartyController : ControllerBase
    {
        private readonly IThirdPartyService thirdPartyService;

        public ThirdPartyController(IThirdPartyService thirdPartyService)
        {
            this.thirdPartyService = thirdPartyService;
        }

        [HttpGet("GetAll")]
        [Authorize("Admin")]
        public ActionResult<ICollection<ThirdParty>> GetAll()
        {
            try
            {
                var result = thirdPartyService.GetAll();
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("ById/{id}")]
        [Authorize]
        public ActionResult<ThirdParty> GetById([FromRoute] int id)
        {
            try
            {
                var result = thirdPartyService.GetById(id);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("ByAccount/{accountId}")]
        [Authorize]
        public ActionResult<ThirdParty> GetByAccountId([FromRoute] int accountId)
        {
            try
            {
                var result = thirdPartyService.GetByAccountId(accountId);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ThirdParty>> Register([FromBody] ThirdPartyRegisterDTO payload)
        {
            try
            {
                var result = await thirdPartyService.AddAsync(payload);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpPut("Activate/{id}")]
        [Authorize("Admin")]
        public async Task<ActionResult<ThirdParty>> Activate([FromRoute] int id)
        {
            try
            {
                var result = await thirdPartyService.ActivateAsync(id);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpPut("Deactivate/{id}")]
        [Authorize]
        public async Task<ActionResult<ThirdParty>> Deactivate([FromRoute] int id)
        {
            try
            {
                var result = await thirdPartyService.DeactivateAsync(id);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpPut("ResetSecret/{id}")]
        [Authorize]
        public async Task<ActionResult<ThirdParty>> ResetClientSecret([FromRoute] int id)
        {
            try
            {
                var result = await thirdPartyService.ResetClientSecretAsync(id);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }
    }
}
