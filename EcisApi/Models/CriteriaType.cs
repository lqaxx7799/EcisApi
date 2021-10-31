using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class CriteriaType : BaseModel
    {
        public int Id { get; set; }
        public string CriteriaTypeName { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Criteria> Criterias { get; set; }
    }
}
