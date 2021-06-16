using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Data
{
    public class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Roles.Any())
            {
                return;   // DB has been seeded
            }

            context.Roles.Add(new Role
            {
                RoleName = "ADMIN",
                CreatedAt = DateTime.Now,
                Description = "Admin Role",
                IsDeleted = false,
                UpdatedAt = DateTime.Now
            });

            context.SaveChanges();
        }
    }
}
