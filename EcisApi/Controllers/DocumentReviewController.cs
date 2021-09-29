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
    public class DocumentReviewController : ControllerBase
    {
        private readonly IDocumentReviewService documentReviewService;

        public DocumentReviewController(
            IDocumentReviewService documentReviewService
            )
        {
            this.documentReviewService = documentReviewService;
        }

        [HttpGet("GetByProcessId/{processId}")]
        [Authorize]
        public ActionResult<ICollection<DocumentReview>> GetByProcessId([FromRoute] int processId)
        {
            return Ok(documentReviewService.GetByProcessId(processId));
        }

        [HttpGet("GetByDocumentId/{documentId}")]
        [Authorize]
        public ActionResult<ICollection<DocumentReview>> GetByDocumentId([FromRoute] int documentId)
        {
            return Ok(documentReviewService.GetByDocumentId(documentId));
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<DocumentReview> GetById([FromRoute] int id)
        {
            return Ok(documentReviewService.GetById(id));
        }

        [HttpPost("Add")]
        [Authorize]
        public async Task<ActionResult<DocumentReview>> Add([FromBody] DocumentReview payload)
        {
            return await documentReviewService.AddAsync(payload);
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<ActionResult<DocumentReview>> Update([FromBody] DocumentReview payload)
        {
            return await documentReviewService.UpdateAsync(payload);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await documentReviewService.DeleteAsync(id);
            return Ok();
        }
    }
}
