using EcisApi.DTO;
using EcisApi.Helpers;
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
    public class FileController : ControllerBase
    {
        private readonly ICloudStorageHelper cloudStorageHelper;

        public FileController(ICloudStorageHelper cloudStorageHelper)
        {
            this.cloudStorageHelper = cloudStorageHelper;
        }

        [HttpPost]
        public ActionResult<string> UploadFile([FromForm] UploadFileDTO data)
        {
            if (data == null || data.File == null || data.File.Length == 0)
            {
                return BadRequest("file not selected");
            }

            var fileName = cloudStorageHelper.UploadFile(data.File);
            return Ok(fileName);
        }
    }
}
