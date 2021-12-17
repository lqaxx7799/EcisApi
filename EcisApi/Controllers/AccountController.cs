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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet("{id}")]
        public ActionResult<Account> GetById([FromRoute]int id)
        {
            return accountService.GetById(id);
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<ActionResult<bool>> ChangePassword([FromBody] ChangePasswordDTO payload)
        {
            var account = (Account)HttpContext.Items["Account"];
            try
            {
                await accountService.ChangePassword(account, payload);
                return Ok(true);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new
                { 
                    e.Message
                });
            }
        }
    }
}
