using EcisApi.Helpers;
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
    public class JobController : ControllerBase
    {
        private readonly IJobService jobService;

        public JobController(IJobService jobService)
        {
            this.jobService = jobService;
        }

        [HttpPost("CheckGenerateVerification")]
        [Authorize("SuperUser")]
        public async Task<ActionResult<bool>> CheckGenerateVerification()
        {
            await jobService.CheckGenerateVerification();
            return Ok(true);
        }

        [HttpPost("CheckVerificationDeadline")]
        [Authorize("SuperUser")]
        public async Task<ActionResult<bool>> CheckVerificationDeadline()
        {
            await jobService.CheckVerificationDeadline();
            return Ok(true);
        }
    }
}
