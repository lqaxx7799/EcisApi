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
        public ActionResult<bool> CheckGenerateVerification()
        {
            Task.Run(() =>
            {
                jobService.CheckGenerateVerification();
            });
            return Ok(true);
        }

        [HttpPost("CheckVerificationDeadline")]
        [Authorize("SuperUser")]
        public ActionResult<bool> CheckVerificationDeadline()
        {
            Task.Run(() =>
            {
                jobService.CheckVerificationDeadline();
            });
            return Ok(true);
        }
    }
}
