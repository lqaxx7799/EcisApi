using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class Account : BaseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public bool IsVerified { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        [JsonIgnore]
        public virtual Company Company { get; set; }
        [JsonIgnore]
        public virtual Agent Agent { get; set; }
        [JsonIgnore]
        public virtual ThirdParty ThirdParty { get; set; }
    }
}
