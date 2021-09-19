using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string CloudBucketName { get; set; }
        
        // Email config
        public string EmailSender { get; set; }
        public string EmailPassword { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPortNumber { get; set; }
    }
}
