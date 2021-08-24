using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Helpers
{
    public interface ICloudStorageHelper
    {
        string UploadFile(IFormFile file);
    }

    public class CloudStorageHelper : ICloudStorageHelper
    {
        private readonly AppSettings _appSettings;

        public CloudStorageHelper(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string UploadFile(IFormFile file)
        {
            string env = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
            var storage = StorageClient.Create();
            var fileName = $"{Guid.NewGuid()}-{file.FileName}";
            var result = storage.UploadObject(_appSettings.CloudBucketName, fileName, null, file.OpenReadStream());
            Console.WriteLine($"Uploaded {fileName}.");
            return fileName;
        }
    }
}
