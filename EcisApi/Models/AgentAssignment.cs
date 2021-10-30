using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class AgentAssignment : BaseModel
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int ProvinceId { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual Province Province { get; set; }
    }
}
