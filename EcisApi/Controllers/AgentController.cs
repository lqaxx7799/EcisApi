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
    public class AgentController : ControllerBase
    {
        private readonly IAgentService agentService;

        public AgentController(
            IAgentService agentService
            )
        {
            this.agentService = agentService;
        }

        [HttpGet("ByAccount/{accountId}")]
        public ActionResult<Agent> GetByCompanyId([FromRoute] int accountId)
        {
            return agentService.GetByAccountId(accountId);
        }

        [HttpGet("{accountId}")]
        public ActionResult<Agent> GetById([FromRoute] int id)
        {
            return agentService.GetById(id);
        }
    }
}
