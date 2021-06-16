using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcisApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Company>().ToTable("Company");
        }
    }
}
