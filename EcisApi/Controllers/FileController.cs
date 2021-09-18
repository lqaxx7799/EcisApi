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

        [HttpGet("{fileName}")]
        public ActionResult GetFile([FromRoute] string fileName)
        {
            try
            {
                var stream = cloudStorageHelper.GetFile(fileName);
                string mimeType = MimeMapping.MimeUtility.GetMimeMapping(fileName);

                return new FileStreamResult(stream, mimeType)
                {
                    FileDownloadName = fileName
                };
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<UploadFileResponseDTO> UploadFile([FromForm] UploadFileDTO data)
        {
            if (data == null || data.File == null || data.File.Length == 0)
            {
                return BadRequest("file not selected");
            }

            var fileName = cloudStorageHelper.UploadFile(data.File);
            var response = new UploadFileResponseDTO
            {
                Name = fileName,
                Size = data.File.Length,
                Type = fileName.Split(".").LastOrDefault(),
                Url = $"/File/{fileName}"
            };
            return Ok(response);
        }
    }
}
