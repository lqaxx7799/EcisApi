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
        public DbSet<CompanyReport> CompanyReports { get; set; }
        public DbSet<CompanyReportDocument> CompanyReportDocuments { get; set; }
        public DbSet<CompanyReportType> CompanyReportTypes { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<CompanyTypeModification> CompanyTypeModifications { get; set; }
        public DbSet<DocumentReview> DocumentReviews { get; set; }
        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<CriteriaType> CriteriaTypes { get; set; }
        public DbSet<ModificationType> ModificationTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<VerificationDocument> VerificationDocuments { get; set; }
        public DbSet<VerificationProcess> VerificationProcesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Account>()
                    .HasOne(s => s.Role)
                    .WithMany(g => g.Accounts)
                    .HasForeignKey(s => s.RoleId);
            modelBuilder.Entity<Agent>().ToTable("Agent");
            modelBuilder.Entity<Company>().ToTable("Company");

            modelBuilder.Entity<CompanyReport>().ToTable("CompanyReport");
            modelBuilder.Entity<CompanyReport>()
                    .HasOne(s => s.CreatorCompany)
                    .WithMany(g => g.CreatorCompanyReports)
                    .HasForeignKey(s => s.CreatorCompanyId);
            modelBuilder.Entity<CompanyReport>()
                   .HasOne(s => s.TargetedCompany)
                   .WithMany(g => g.TargetedCompanyReports)
                   .HasForeignKey(s => s.TargetedCompanyId);

            modelBuilder.Entity<CompanyReportDocument>().ToTable("CompanyReportDocument");
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
            modelBuilder.Entity<Criteria>().ToTable("Criteria");
            modelBuilder.Entity<CriteriaType>().ToTable("CriteriaType");
            modelBuilder.Entity<ModificationType>().ToTable("ModificationType");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<VerificationDocument>().ToTable("VerificationDocument");

            modelBuilder.Entity<VerificationProcess>().ToTable("VerificationProcess");
            modelBuilder.Entity<VerificationProcess>()
                    .HasOne(s => s.Company)
                    .WithMany(g => g.VerificationProcesses)
                    .HasForeignKey(s => s.CompanyId)
                    .IsRequired(false);
        }
    }
}
