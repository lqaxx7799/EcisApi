using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        [JsonIgnore]
        public Role Role { get; set; }
        [JsonIgnore]
        public Company Company { get; set; }
        [JsonIgnore]
        public Agent Agent { get; set; }
    }
}
