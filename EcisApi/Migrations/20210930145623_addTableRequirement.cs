using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcisApi.Migrations
{
    public partial class addTableRequirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VerificationConfirmRequirement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduledTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnouncedAgentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnnouncedCompanyAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnnounceAgentDocumentContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnounceAgentDocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnounceAgentDocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnounceAgentDocumentSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnounceAgentDocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUsingAnnounceAgentFile = table.Column<bool>(type: "bit", nullable: false),
                    AnnounceCompanyDocumentContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnounceCompanyDocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnounceCompanyDocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnounceCompanyDocumentSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnounceCompanyDocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUsingAnnounceCompanyFile = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmDocumentContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmDocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmDocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmDocumentSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmDocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUsingConfirmFile = table.Column<bool>(type: "bit", nullable: false),
                    VerificationProcessId = table.Column<int>(type: "int", nullable: true),
                    AssignedAgentId = table.Column<int>(type: "int", nullable: true),
                    ConfirmCompanyTypeId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationConfirmRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerificationConfirmRequirement_Agent_AssignedAgentId",
                        column: x => x.AssignedAgentId,
                        principalTable: "Agent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VerificationConfirmRequirement_CompanyType_ConfirmCompanyTypeId",
                        column: x => x.ConfirmCompanyTypeId,
                        principalTable: "CompanyType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VerificationConfirmRequirement_VerificationProcess_VerificationProcessId",
                        column: x => x.VerificationProcessId,
                        principalTable: "VerificationProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VerificationConfirmRequirement_AssignedAgentId",
                table: "VerificationConfirmRequirement",
                column: "AssignedAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationConfirmRequirement_ConfirmCompanyTypeId",
                table: "VerificationConfirmRequirement",
                column: "ConfirmCompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationConfirmRequirement_VerificationProcessId",
                table: "VerificationConfirmRequirement",
                column: "VerificationProcessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerificationConfirmRequirement");
        }
    }
}
