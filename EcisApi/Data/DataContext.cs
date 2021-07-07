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

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyAction> CompanyActions { get; set; }
        public DbSet<CompanyActionAttachment> CompanyActionAttachments { get; set; }
        public DbSet<CompanyActionType> CompanyActionTypes { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<CompanyTypeModification> CompanyTypeModifications { get; set; }
        public DbSet<DocumentReview> DocumentReviews { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<ModificationType> ModificationTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<VerificationDocument> VerificationDocuments { get; set; }
        public DbSet<VerificationProcess> VerificationProcesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Account>().ToTable("Agent");
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<Account>().ToTable("CompanyAction");
            modelBuilder.Entity<Account>().ToTable("CompanyActionAttachment");
            modelBuilder.Entity<Account>().ToTable("CompanyType");
            modelBuilder.Entity<Account>().ToTable("CompanyTypeModification");
            modelBuilder.Entity<Account>().ToTable("DocumentReview");
            modelBuilder.Entity<Account>().ToTable("DocumentType");
            modelBuilder.Entity<Account>().ToTable("ModificationType");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Account>().ToTable("VerificationDocument");
            modelBuilder.Entity<Account>().ToTable("VerificationProcess");
        }
    }
}
