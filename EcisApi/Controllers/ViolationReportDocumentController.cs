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
    public class ViolationReportDocumentController : ControllerBase
    {
        private readonly IViolationReportDocumentService violationReportDocumentService;

        public ViolationReportDocumentController(IViolationReportDocumentService violationReportDocumentService)
        {
            this.violationReportDocumentService = violationReportDocumentService;
        }

        [HttpGet("GetByReport/{id}")]
        [Authorize]
        public ActionResult<ICollection<ViolationReportDocument>> GetByReport([FromRoute] int id)
        {
            return Ok(violationReportDocumentService.GetByReportId(id));
        }
    }
}
