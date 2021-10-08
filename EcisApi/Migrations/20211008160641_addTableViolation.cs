using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcisApi.Migrations
{
    public partial class addTableViolation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViolationReportId",
                table: "CompanyTypeModification",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ViolationReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    ReportAgentId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViolationReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViolationReport_Agent_ReportAgentId",
                        column: x => x.ReportAgentId,
                        principalTable: "Agent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ViolationReport_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ViolationReportDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentSize = table.Column<long>(type: "bigint", nullable: false),
                    ViolationReportId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViolationReportDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViolationReportDocument_ViolationReport_ViolationReportId",
                        column: x => x.ViolationReportId,
                        principalTable: "ViolationReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTypeModification_ViolationReportId",
                table: "CompanyTypeModification",
                column: "ViolationReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ViolationReport_CompanyId",
                table: "ViolationReport",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ViolationReport_ReportAgentId",
                table: "ViolationReport",
                column: "ReportAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_ViolationReportDocument_ViolationReportId",
                table: "ViolationReportDocument",
                column: "ViolationReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTypeModification_ViolationReport_ViolationReportId",
                table: "CompanyTypeModification",
                column: "ViolationReportId",
                principalTable: "ViolationReport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTypeModification_ViolationReport_ViolationReportId",
                table: "CompanyTypeModification");

            migrationBuilder.DropTable(
                name: "ViolationReportDocument");

            migrationBuilder.DropTable(
                name: "ViolationReport");

            migrationBuilder.DropIndex(
                name: "IX_CompanyTypeModification_ViolationReportId",
                table: "CompanyTypeModification");

            migrationBuilder.DropColumn(
                name: "ViolationReportId",
                table: "CompanyTypeModification");
        }
    }
}
