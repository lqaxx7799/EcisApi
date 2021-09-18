using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class UploadFileResponseDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
        public string Url { get; set; }
    }
}
