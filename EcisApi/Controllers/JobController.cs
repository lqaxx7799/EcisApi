using EcisApi.Helpers;
using EcisApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IServiceScopeFactory serviceScopeFactory;

        public JobController(
            IServiceScopeFactory serviceScopeFactory
            )
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        [HttpPost("CheckGenerateVerification")]
        [Authorize("SuperUser")]
        public ActionResult<bool> CheckGenerateVerification()
        {
            Task.Run(async () =>
            {
                try
                {
                    using var scope = serviceScopeFactory.CreateScope();
                    var jobService = scope.ServiceProvider.GetRequiredService<IJobService>();
                    await jobService.CheckGenerateVerification();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            });
            return Ok(true);
        }

        [HttpPost("CheckVerificationDeadline")]
        [Authorize("SuperUser")]
        public ActionResult<bool> CheckVerificationDeadline()
        {
            Task.Run(async () =>
            {
                try
                {
                    using var scope = serviceScopeFactory.CreateScope();
                    var jobService = scope.ServiceProvider.GetRequiredService<IJobService>();
                    await jobService.CheckVerificationDeadline();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            });
            return Ok(true);
        }
    }
}
