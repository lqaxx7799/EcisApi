﻿// <auto-generated />
using System;
using EcisApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EcisApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210919075350_updateDocument")]
    partial class updateDocument
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EcisApi.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("EcisApi.Models.Agent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Agent");
                });

            modelBuilder.Entity("EcisApi.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("CompanyCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyNameEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyNameVI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CompanyTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LogoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("CompanyTypeId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("EcisApi.Models.CompanyReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AcceptedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ActionTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AssignedAgentId")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyReportTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatorCompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HandledAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsHandled")
                        .HasColumnType("bit");

                    b.Property<int?>("TargetedCompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("VerificationProcessId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssignedAgentId");

                    b.HasIndex("CompanyReportTypeId");

                    b.HasIndex("CreatorCompanyId");

                    b.HasIndex("TargetedCompanyId");

                    b.HasIndex("VerificationProcessId");

                    b.ToTable("CompanyReport");
                });

            modelBuilder.Entity("EcisApi.Models.CompanyReportDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyReportId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ResourceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CompanyReportId");

                    b.ToTable("CompanyReportDocument");
                });

            modelBuilder.Entity("EcisApi.Models.CompanyReportType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("CompanyReportTypes");
                });

            modelBuilder.Entity("EcisApi.Models.CompanyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("CompanyType");
                });

            modelBuilder.Entity("EcisApi.Models.CompanyTypeModification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyReportId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("ModificationTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("PreviousCompanyTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedCompanyTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("VerificationProcessId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CompanyReportId");

                    b.HasIndex("ModificationTypeId");

                    b.HasIndex("PreviousCompanyTypeId");

                    b.HasIndex("UpdatedCompanyTypeId");

                    b.HasIndex("VerificationProcessId");

                    b.ToTable("CompanyTypeModification");
                });

            modelBuilder.Entity("EcisApi.Models.Criteria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CriteriaName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CriteriaTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CriteriaTypeId");

                    b.ToTable("Criteria");
                });

            modelBuilder.Entity("EcisApi.Models.CriteriaType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CriteriaTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("CriteriaType");
                });

            modelBuilder.Entity("EcisApi.Models.DocumentReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("VerificationDocumentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VerificationDocumentId");

                    b.ToTable("DocumentReview");
                });

            modelBuilder.Entity("EcisApi.Models.ModificationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ModificationType");
                });

            modelBuilder.Entity("EcisApi.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("EcisApi.Models.VerificationCriteria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApprovedStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CriteriaId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("VerificationProcessId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CriteriaId");

                    b.HasIndex("VerificationProcessId");

                    b.ToTable("VerificationCriteria");
                });

            modelBuilder.Entity("EcisApi.Models.VerificationDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long>("ResourceSize")
                        .HasColumnType("bigint");

                    b.Property<string>("ResourceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UploaderType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VerificationCriteriaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VerificationCriteriaId");

                    b.ToTable("VerificationDocument");
                });

            modelBuilder.Entity("EcisApi.Models.VerificationProcess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssignedAgentId")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOpenedByAgent")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSubmitted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ReviewedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SubmitDeadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubmitMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SubmittedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ValidFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ValidTo")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AssignedAgentId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CompanyTypeId");

                    b.ToTable("VerificationProcess");
                });

            modelBuilder.Entity("EcisApi.Models.Account", b =>
                {
                    b.HasOne("EcisApi.Models.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("EcisApi.Models.Agent", b =>
                {
                    b.HasOne("EcisApi.Models.Account", "Account")
                        .WithOne("Agent")
                        .HasForeignKey("EcisApi.Models.Agent", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("EcisApi.Models.Company", b =>
                {
                    b.HasOne("EcisApi.Models.Account", "Account")
                        .WithOne("Company")
                        .HasForeignKey("EcisApi.Models.Company", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcisApi.Models.CompanyType", "CompanyType")
                        .WithMany("Companies")
                        .HasForeignKey("CompanyTypeId");

                    b.Navigation("Account");

                    b.Navigation("CompanyType");
                });

            modelBuilder.Entity("EcisApi.Models.CompanyReport", b =>
                {
                    b.HasOne("EcisApi.Models.Agent", "AssignedAgent")
                        .WithMany("CompanyActions")
                        .HasForeignKey("AssignedAgentId");

                    b.HasOne("EcisApi.Models.CompanyReportType", "CompanyReportType")
                        .WithMany("CompanyReports")
                        .HasForeignKey("CompanyReportTypeId");

                    b.HasOne("EcisApi.Models.Company", "CreatorCompany")
                        .WithMany("CreatorCompanyReports")
                        .HasForeignKey("CreatorCompanyId");

                    b.HasOne("EcisApi.Models.Company", "TargetedCompany")
                        .WithMany("TargetedCompanyReports")
                        .HasForeignKey("TargetedCompanyId");

                    b.HasOne("EcisApi.Models.VerificationProcess", "VerificationProcess")
                        .WithMany("CompanyReports")
                        .HasForeignKey("VerificationProcessId");

                    b.Navigation("AssignedAgent");

                    b.Navigation("CompanyReportType");

                    b.Navigation("CreatorCompany");

                    b.Navigation("TargetedCompany");

                    b.Navigation("VerificationProcess");
                });

            modelBuilder.Entity("EcisApi.Models.CompanyReportDocument", b =>
                {
                    b.HasOne("EcisApi.Models.CompanyReport", "CompanyReport")
                        .WithMany("CompanyReportDocuments")
                        .HasForeignKey("CompanyReportId");

                    b.Navigation("CompanyReport");
                });

            modelBuilder.Entity("EcisApi.Models.CompanyTypeModification", b =>
                {
                    b.HasOne("EcisApi.Models.Company", "Company")
                        .WithMany("CompanyTypeModifications")
                        .HasForeignKey("CompanyId");

                    b.HasOne("EcisApi.Models.CompanyReport", "CompanyReport")
                        .WithMany("CompanyTypeModifications")
                        .HasForeignKey("CompanyReportId");

                    b.HasOne("EcisApi.Models.ModificationType", "ModificationType")
                        .WithMany("CompanyTypeModifications")
                        .HasForeignKey("ModificationTypeId");

                    b.HasOne("EcisApi.Models.CompanyType", "PreviousCompanyType")
                        .WithMany("PreviousCompanyTypeModifications")
                        .HasForeignKey("PreviousCompanyTypeId");

                    b.HasOne("EcisApi.Models.CompanyType", "UpdatedCompanyType")
                        .WithMany("UpdatedCompanyTypeModifications")
                        .HasForeignKey("UpdatedCompanyTypeId");

                    b.HasOne("EcisApi.Models.VerificationProcess", "VerificationProcess")
                        .WithMany("CompanyTypeModifications")
                        .HasForeignKey("VerificationProcessId");

                    b.Navigation("Company");

                    b.Navigation("CompanyReport");

                    b.Navigation("ModificationType");

                    b.Navigation("PreviousCompanyType");

                    b.Navigation("UpdatedCompanyType");

                    b.Navigation("VerificationProcess");
                });

            modelBuilder.Entity("EcisApi.Models.Criteria", b =>
                {
                    b.HasOne("EcisApi.Models.CriteriaType", "CriteriaType")
                        .WithMany("Criterias")
                        .HasForeignKey("CriteriaTypeId");

                    b.Navigation("CriteriaType");
                });

            modelBuilder.Entity("EcisApi.Models.DocumentReview", b =>
                {
                    b.HasOne("EcisApi.Models.VerificationDocument", "VerificationDocument")
                        .WithMany("DocumentReviews")
                        .HasForeignKey("VerificationDocumentId");

                    b.Navigation("VerificationDocument");
                });

            modelBuilder.Entity("EcisApi.Models.VerificationCriteria", b =>
                {
                    b.HasOne("EcisApi.Models.Criteria", "Criteria")
                        .WithMany("VerificationCriterias")
                        .HasForeignKey("CriteriaId");

                    b.HasOne("EcisApi.Models.VerificationProcess", "VerificationProcess")
                        .WithMany("VerificationCriterias")
                        .HasForeignKey("VerificationProcessId");

                    b.Navigation("Criteria");

                    b.Navigation("VerificationProcess");
                });

            modelBuilder.Entity("EcisApi.Models.VerificationDocument", b =>
                {
                    b.HasOne("EcisApi.Models.VerificationCriteria", "VerificationCriteria")
                        .WithMany("VerificationDocuments")
                        .HasForeignKey("VerificationCriteriaId");

                    b.Navigation("VerificationCriteria");
                });

            modelBuilder.Entity("EcisApi.Models.VerificationProcess", b =>
                {
                    b.HasOne("EcisApi.Models.Agent", "AssignedAgent")
                        .WithMany("VerificationProcesses")
                        .HasForeignKey("AssignedAgentId");

                    b.HasOne("EcisApi.Models.Company", "Company")
                        .WithMany("VerificationProcesses")
                        .HasForeignKey("CompanyId");

                    b.HasOne("EcisApi.Models.CompanyType", "CompanyType")
                        .WithMany("VerificationProcesses")
                        .HasForeignKey("CompanyTypeId");

                    b.Navigation("AssignedAgent");

                    b.Navigation("Company");

                    b.Navigation("CompanyType");
                });

            modelBuilder.Entity("EcisApi.Models.Account", b =>
                {
                    b.Navigation("Agent");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("EcisApi.Models.Agent", b =>
                {
                    b.Navigation("CompanyActions");

                    b.Navigation("VerificationProcesses");
                });

            modelBuilder.Entity("EcisApi.Models.Company", b =>
                {
                    b.Navigation("CompanyTypeModifications");

                    b.Navigation("CreatorCompanyReports");

                    b.Navigation("TargetedCompanyReports");

                    b.Navigation("VerificationProcesses");
                });

            modelBuilder.Entity("EcisApi.Models.CompanyReport", b =>
                {
                    b.Navigation("CompanyReportDocuments");

                    b.Navigation("CompanyTypeModifications");
                });

            modelBuilder.Entity("EcisApi.Models.CompanyReportType", b =>
                {
                    b.Navigation("CompanyReports");
                });

            modelBuilder.Entity("EcisApi.Models.CompanyType", b =>
                {
                    b.Navigation("Companies");

                    b.Navigation("PreviousCompanyTypeModifications");

                    b.Navigation("UpdatedCompanyTypeModifications");

                    b.Navigation("VerificationProcesses");
                });

            modelBuilder.Entity("EcisApi.Models.Criteria", b =>
                {
                    b.Navigation("VerificationCriterias");
                });

            modelBuilder.Entity("EcisApi.Models.CriteriaType", b =>
                {
                    b.Navigation("Criterias");
                });

            modelBuilder.Entity("EcisApi.Models.ModificationType", b =>
                {
                    b.Navigation("CompanyTypeModifications");
                });

            modelBuilder.Entity("EcisApi.Models.Role", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("EcisApi.Models.VerificationCriteria", b =>
                {
                    b.Navigation("VerificationDocuments");
                });

            modelBuilder.Entity("EcisApi.Models.VerificationDocument", b =>
                {
                    b.Navigation("DocumentReviews");
                });

            modelBuilder.Entity("EcisApi.Models.VerificationProcess", b =>
                {
                    b.Navigation("CompanyReports");

                    b.Navigation("CompanyTypeModifications");

                    b.Navigation("VerificationCriterias");
                });
#pragma warning restore 612, 618
        }
    }
}
