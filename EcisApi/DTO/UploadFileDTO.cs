using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class UploadFileDTO
    {
        public IFormFile File { get; set; }
    }
}
