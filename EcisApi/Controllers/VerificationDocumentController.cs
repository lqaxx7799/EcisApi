using EcisApi.Helpers;
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
    public class VerificationDocumentController : ControllerBase
    {
        private readonly IVerificationDocumentService verificationDocumentService;

        public VerificationDocumentController(IVerificationDocumentService verificationDocumentService)
        {
            this.verificationDocumentService = verificationDocumentService;
        }

        [HttpGet("GetByProcessId/{processId}")]
        [Authorize]
        public ActionResult<ICollection<VerificationDocument>> GetByProcessId([FromRoute] int processId)
        {
            return Ok(verificationDocumentService.GetByProcessId(processId));
        }

        [HttpPost("Add")]
        [Authorize]
        public async Task<ActionResult<VerificationDocument>> Add([FromBody] VerificationDocument payload)
        {
            return await verificationDocumentService.AddAsync(payload);
        }

        [HttpPost("AddBatch")]
        [Authorize]
        public async Task<ActionResult<ICollection<VerificationDocument>>> AddBatch([FromBody] ICollection<VerificationDocument> payload)
        {
            var data = await verificationDocumentService.AddBatchAsync(payload);
            return Ok(data);
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<ActionResult<VerificationDocument>> Update([FromBody] VerificationDocument payload)
        {
            return await verificationDocumentService.UpdateAsync(payload);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await verificationDocumentService.Delete(id);
            return Ok();
        }
    }
}
