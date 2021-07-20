using EcisApi.Helpers;
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

            var adminRole = new Role
            {
                RoleName = "ADMIN",
                CreatedAt = DateTime.Now,
                Description = "Admin Role",
                IsDeleted = false,
                UpdatedAt = DateTime.Now
            };
            context.Roles.Add(adminRole);

            context.Accounts.Add(new Account
            {
                Email = "qanh@gmail.com",
                Password = CommonUtils.GenerateSHA1("abcd1234"),
                Role = adminRole,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsVerified = true
            });

            context.SaveChanges();
        }
    }
}
