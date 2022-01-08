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
        public DbSet<AgentAssignment> AgentAssignments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyReport> CompanyReports { get; set; }
        public DbSet<CompanyReportDocument> CompanyReportDocuments { get; set; }
        public DbSet<CompanyReportType> CompanyReportTypes { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<CompanyTypeModification> CompanyTypeModifications { get; set; }
        public DbSet<DocumentReview> DocumentReviews { get; set; }
        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<CriteriaDetail> CriteriaDetails { get; set; }
        public DbSet<CriteriaType> CriteriaTypes { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SystemConfiguration> SystemConfigurations { get; set; }
        public DbSet<ThirdParty> ThirdParties { get; set; }
        public DbSet<VerificationConfirmRequirement> VerificationConfirmRequirements { get; set; }
        public DbSet<VerificationConfirmDocument> VerificationConfirmDocuments { get; set; }
        public DbSet<VerificationCriteria> VerificationCriterias { get; set; }
        public DbSet<VerificationDocument> VerificationDocuments { get; set; }
        public DbSet<VerificationProcess> VerificationProcesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Account>()
                .HasIndex(x => x.Email);

            modelBuilder.Entity<Agent>().ToTable("Agent");
            modelBuilder.Entity<AgentAssignment>().ToTable("AgentAssignment");

            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<Company>()
                .HasIndex(x => x.CompanyCode);

            modelBuilder.Entity<CompanyReport>().ToTable("CompanyReport");
            modelBuilder.Entity<CompanyReport>()
                .HasOne(s => s.CreatorCompany)
                .WithMany(g => g.CreatorCompanyReports)
                .HasForeignKey(s => s.CreatorCompanyId);
            modelBuilder.Entity<CompanyReport>()
                .HasOne(s => s.TargetedCompany)
                .WithMany(g => g.TargetedCompanyReports)
                .HasForeignKey(s => s.TargetedCompanyId);
            modelBuilder.Entity<CompanyReport>()
                .HasIndex(x => new { x.Status, x.IsDeleted });

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
            modelBuilder.Entity<CriteriaDetail>().ToTable("CriteriaDetail");
            modelBuilder.Entity<CriteriaType>().ToTable("CriteriaType");
            modelBuilder.Entity<Province>().ToTable("Province");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<SystemConfiguration>().ToTable("SystemConfiguration");
            modelBuilder.Entity<ThirdParty>().ToTable("ThirdParty");
            modelBuilder.Entity<VerificationConfirmDocument>().ToTable("VerificationConfirmDocument");

            modelBuilder.Entity<VerificationConfirmRequirement>().ToTable("VerificationConfirmRequirement");
            modelBuilder.Entity<VerificationConfirmRequirement>()
                .HasOne(s => s.AssignedAgent)
                .WithMany(g => g.VerificationConfirmRequirements)
                .HasForeignKey(s => s.AssignedAgentId);
            modelBuilder.Entity<VerificationConfirmRequirement>()
                .HasOne(s => s.ConfirmCompanyType)
                .WithMany(g => g.VerificationConfirmRequirements)
                .HasForeignKey(s => s.ConfirmCompanyTypeId);

            modelBuilder.Entity<VerificationCriteria>().ToTable("VerificationCriteria");
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
