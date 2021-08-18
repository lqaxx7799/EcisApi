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

        [HttpPost("Add")]
        public async Task<ActionResult<VerificationDocument>> Add([FromBody] VerificationDocument payload)
        {
            return await verificationDocumentService.AddAsync(payload);
        }

        [HttpPost("AddBatch")]
        public async Task<ActionResult<ICollection<VerificationDocument>>> AddBatch([FromBody] ICollection<VerificationDocument> payload)
        {
            var data = await verificationDocumentService.AddBatchAsync(payload);
            return Ok(data);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<VerificationDocument>> Update([FromBody] VerificationDocument payload)
        {
            return await verificationDocumentService.UpdateAsync(payload);
        }
    }
}
