using EcisApi.DTO;
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
    public class AuthenticationController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AuthenticationController(
            IAccountService accountService
            )
        {
            this.accountService = accountService;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateRequestDTO model)
        {
            var response = accountService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("AuthenticateManagement")]
        public IActionResult AuthenticateManagement([FromBody] AuthenticateRequestDTO model)
        {
            try
            {
                var response = accountService.AuthenticateManagement(model);
                return Ok(response);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet("Validate")]
        public ActionResult<Account> Validate()
        {
            var account = (Account)HttpContext.Items["Account"];
            if (account == null)
            {
                return Unauthorized();
            }
            return Ok(account);
        }
    }
}
