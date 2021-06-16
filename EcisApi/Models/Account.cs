using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class Account
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public int RoleID { get; set; }

        public Role Role { get; set; }
        public Company Company { get; set; }
    }
}
