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
    public class AgentController : ControllerBase
    {
        private readonly IAgentService agentService;

        public AgentController(
            IAgentService agentService
            )
        {
            this.agentService = agentService;
        }

        [HttpGet("GetAll")]
        [Authorize]
        public ActionResult<ICollection<Agent>> GetAll()
        {
            return Ok(agentService.GetAll());
        }

        [HttpGet("GetAllAgents")]
        [Authorize]
        public ActionResult<ICollection<Agent>> GetAllAgents()
        {
            return Ok(agentService.GetAllAgents());
        }

        [HttpGet("ByAccount/{accountId}")]
        [Authorize]
        public ActionResult<Agent> GetByCompanyId([FromRoute] int accountId)
        {
            return agentService.GetByAccountId(accountId);
        }

        [HttpGet("GetAssignments/{agentId}")]
        [Authorize]
        public ActionResult<ICollection<AgentAssignment>> GetAssignmentsByAgentId([FromRoute] int agentId)
        {
            return Ok(agentService.GetAssignmentsByAgentId(agentId));
        }

        [HttpGet("ById/{id}")]
        [Authorize]
        public ActionResult<Agent> GetById([FromRoute] int id)
        {
            return agentService.GetById(id);
        }

        [HttpPost("Add")]
        [Authorize("Admin")]
        public async Task<ActionResult<Agent>> Add([FromBody] AgentCreateDTO payload)
        {
            try
            {
                var result = await agentService.AddAsync(payload);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }
    }
}
