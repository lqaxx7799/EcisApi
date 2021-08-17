using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcisApi.Migrations
{
    public partial class UpdateDB1708 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTypeModification_CompanyAction_CompanyActionId",
                table: "CompanyTypeModification");

            migrationBuilder.DropForeignKey(
                name: "FK_VerificationDocument_DocumentType_DocumentTypeId",
                table: "VerificationDocument");

            migrationBuilder.DropTable(
                name: "CompanyActionAttachment");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropTable(
                name: "CompanyAction");

            migrationBuilder.DropTable(
                name: "CompanyActionTypes");

            migrationBuilder.RenameColumn(
                name: "DocumentTypeId",
                table: "VerificationDocument",
                newName: "CriteriaId");

            migrationBuilder.RenameIndex(
                name: "IX_VerificationDocument_DocumentTypeId",
                table: "VerificationDocument",
                newName: "IX_VerificationDocument_CriteriaId");

            migrationBuilder.RenameColumn(
                name: "CompanyActionId",
                table: "CompanyTypeModification",
                newName: "CompanyReportId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyTypeModification_CompanyActionId",
                table: "CompanyTypeModification",
                newName: "IX_CompanyTypeModification_CompanyReportId");

            migrationBuilder.CreateTable(
                name: "CompanyReportTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyReportTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CriteriaType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriteriaTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriteriaType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcceptedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HandledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsHandled = table.Column<bool>(type: "bit", nullable: false),
                    VerificationProcessId = table.Column<int>(type: "int", nullable: true),
                    CompanyReportTypeId = table.Column<int>(type: "int", nullable: true),
                    TargetedCompanyId = table.Column<int>(type: "int", nullable: true),
                    CreatorCompanyId = table.Column<int>(type: "int", nullable: true),
                    AssignedAgentId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyReport_Agent_AssignedAgentId",
                        column: x => x.AssignedAgentId,
                        principalTable: "Agent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyReport_Company_CreatorCompanyId",
                        column: x => x.CreatorCompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyReport_Company_TargetedCompanyId",
                        column: x => x.TargetedCompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyReport_CompanyReportTypes_CompanyReportTypeId",
                        column: x => x.CompanyReportTypeId,
                        principalTable: "CompanyReportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyReport_VerificationProcess_VerificationProcessId",
                        column: x => x.VerificationProcessId,
                        principalTable: "VerificationProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Criteria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriteriaName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CriteriaTypeId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Criteria_CriteriaType_CriteriaTypeId",
                        column: x => x.CriteriaTypeId,
                        principalTable: "CriteriaType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyReportDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyReportId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyReportDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyReportDocument_CompanyReport_CompanyReportId",
                        column: x => x.CompanyReportId,
                        principalTable: "CompanyReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyReport_AssignedAgentId",
                table: "CompanyReport",
                column: "AssignedAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyReport_CompanyReportTypeId",
                table: "CompanyReport",
                column: "CompanyReportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyReport_CreatorCompanyId",
                table: "CompanyReport",
                column: "CreatorCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyReport_TargetedCompanyId",
                table: "CompanyReport",
                column: "TargetedCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyReport_VerificationProcessId",
                table: "CompanyReport",
                column: "VerificationProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyReportDocument_CompanyReportId",
                table: "CompanyReportDocument",
                column: "CompanyReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Criteria_CriteriaTypeId",
                table: "Criteria",
                column: "CriteriaTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTypeModification_CompanyReport_CompanyReportId",
                table: "CompanyTypeModification",
                column: "CompanyReportId",
                principalTable: "CompanyReport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationDocument_Criteria_CriteriaId",
                table: "VerificationDocument",
                column: "CriteriaId",
                principalTable: "Criteria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTypeModification_CompanyReport_CompanyReportId",
                table: "CompanyTypeModification");

            migrationBuilder.DropForeignKey(
                name: "FK_VerificationDocument_Criteria_CriteriaId",
                table: "VerificationDocument");

            migrationBuilder.DropTable(
                name: "CompanyReportDocument");

            migrationBuilder.DropTable(
                name: "Criteria");

            migrationBuilder.DropTable(
                name: "CompanyReport");

            migrationBuilder.DropTable(
                name: "CriteriaType");

            migrationBuilder.DropTable(
                name: "CompanyReportTypes");

            migrationBuilder.RenameColumn(
                name: "CriteriaId",
                table: "VerificationDocument",
                newName: "DocumentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_VerificationDocument_CriteriaId",
                table: "VerificationDocument",
                newName: "IX_VerificationDocument_DocumentTypeId");

            migrationBuilder.RenameColumn(
                name: "CompanyReportId",
                table: "CompanyTypeModification",
                newName: "CompanyActionId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyTypeModification_CompanyReportId",
                table: "CompanyTypeModification",
                newName: "IX_CompanyTypeModification_CompanyActionId");

            migrationBuilder.CreateTable(
                name: "CompanyActionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyActionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcceptedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedAgentId = table.Column<int>(type: "int", nullable: true),
                    CompanyActionTypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorCompanyId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HandledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsHandled = table.Column<bool>(type: "bit", nullable: false),
                    TargetedCompanyId = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VerificationProcessId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyAction_Agent_AssignedAgentId",
                        column: x => x.AssignedAgentId,
                        principalTable: "Agent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyAction_Company_CreatorCompanyId",
                        column: x => x.CreatorCompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyAction_Company_TargetedCompanyId",
                        column: x => x.TargetedCompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyAction_CompanyActionTypes_CompanyActionTypeId",
                        column: x => x.CompanyActionTypeId,
                        principalTable: "CompanyActionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyAction_VerificationProcess_VerificationProcessId",
                        column: x => x.VerificationProcessId,
                        principalTable: "VerificationProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyActionAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyActionId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ResourceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyActionAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyActionAttachment_CompanyAction_CompanyActionId",
                        column: x => x.CompanyActionId,
                        principalTable: "CompanyAction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAction_AssignedAgentId",
                table: "CompanyAction",
                column: "AssignedAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAction_CompanyActionTypeId",
                table: "CompanyAction",
                column: "CompanyActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAction_CreatorCompanyId",
                table: "CompanyAction",
                column: "CreatorCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAction_TargetedCompanyId",
                table: "CompanyAction",
                column: "TargetedCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAction_VerificationProcessId",
                table: "CompanyAction",
                column: "VerificationProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyActionAttachment_CompanyActionId",
                table: "CompanyActionAttachment",
                column: "CompanyActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTypeModification_CompanyAction_CompanyActionId",
                table: "CompanyTypeModification",
                column: "CompanyActionId",
                principalTable: "CompanyAction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationDocument_DocumentType_DocumentTypeId",
                table: "VerificationDocument",
                column: "DocumentTypeId",
                principalTable: "DocumentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
