using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class VerificationDocument : BaseModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string DocumentName { get; set; }
        public string ResourceType { get; set; }
        public string ResourceUrl { get; set; }
        public long ResourceSize { get; set; }
        public string UploaderType { get; set; }
        
        public int? VerificationCriteriaId { get; set; }

        public virtual VerificationCriteria VerificationCriteria { get; set; }

        [JsonIgnore]
        public virtual ICollection<DocumentReview> DocumentReviews { get; set; }
    }
}
