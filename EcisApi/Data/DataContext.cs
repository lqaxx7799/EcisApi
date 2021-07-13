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
            modelBuilder.Entity<Agent>().ToTable("Agent");
            modelBuilder.Entity<Company>().ToTable("Company");

            modelBuilder.Entity<CompanyAction>().ToTable("CompanyAction");
            modelBuilder.Entity<CompanyAction>()
                    .HasOne(s => s.CreatorCompany)
                    .WithMany(g => g.CreatorCompanyActions)
                    .HasForeignKey(s => s.CreatorCompanyId);
            modelBuilder.Entity<CompanyAction>()
                   .HasOne(s => s.TargetedCompany)
                   .WithMany(g => g.TargetedCompanyActions)
                   .HasForeignKey(s => s.TargetedCompanyId);

            modelBuilder.Entity<CompanyActionAttachment>().ToTable("CompanyActionAttachment");
            modelBuilder.Entity<CompanyType>().ToTable("CompanyType");

            modelBuilder.Entity<CompanyTypeModification>().ToTable("CompanyTypeModification");
            modelBuilder.Entity<CompanyTypeModification>()
                   .HasOne(s => s.PreviousCompanyType)
                   .WithMany(g => g.PreviousCompanyTypeModifications)
                   .HasForeignKey(s => s.PreviousCompanyTypeId);
            modelBuilder.Entity<CompanyTypeModification>()
                   .HasOne(s => s.UpdatedCompanyType)
                   .WithMany(g => g.UpdatedCompanyTypeModifications)
                   .HasForeignKey(s => s.UpdatedCompanyTypeId);

            modelBuilder.Entity<DocumentReview>().ToTable("DocumentReview");
            modelBuilder.Entity<DocumentType>().ToTable("DocumentType");
            modelBuilder.Entity<ModificationType>().ToTable("ModificationType");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<VerificationDocument>().ToTable("VerificationDocument");
            modelBuilder.Entity<VerificationProcess>().ToTable("VerificationProcess");
        }
    }
}
