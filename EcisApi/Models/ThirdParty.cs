using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class ThirdParty : BaseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool IsActive { get; set; }

        public int AccountId { get; set; }

        public virtual Account Account { get; set; }
    }
}
