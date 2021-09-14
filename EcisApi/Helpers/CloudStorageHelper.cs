using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Helpers
{
    public interface ICloudStorageHelper
    {
        Stream GetFile(string fileName);
        string UploadFile(IFormFile file);
    }

    public class CloudStorageHelper : ICloudStorageHelper
    {
        private readonly AppSettings _appSettings;

        public CloudStorageHelper(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public Stream GetFile(string fileName)
        {
            var stream = new MemoryStream();
            var storage = StorageClient.Create();
            storage.DownloadObject(_appSettings.CloudBucketName, fileName, stream);
            stream.Position = 0;
            return stream;
        }

        public string UploadFile(IFormFile file)
        {
            var storage = StorageClient.Create();
            var fileName = $"{Guid.NewGuid()}-{file.FileName}";
            var result = storage.UploadObject(_appSettings.CloudBucketName, fileName, null, file.OpenReadStream());
            Console.WriteLine($"Uploaded {fileName}.");
            return fileName;
        }
    }
}
