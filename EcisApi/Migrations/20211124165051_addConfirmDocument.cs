using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcisApi.Migrations
{
    public partial class addConfirmDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnounceAgentDocumentName",
                table: "VerificationConfirmRequirement");

            migrationBuilder.DropColumn(
                name: "AnnounceAgentDocumentSize",
                table: "VerificationConfirmRequirement");

            migrationBuilder.DropColumn(
                name: "AnnounceAgentDocumentType",
                table: "VerificationConfirmRequirement");

            migrationBuilder.DropColumn(
                name: "AnnounceAgentDocumentUrl",
                table: "VerificationConfirmRequirement");

            migrationBuilder.DropColumn(
                name: "AnnounceCompanyDocumentContent",
                table: "VerificationConfirmRequirement");

            migrationBuilder.DropColumn(
                name: "AnnounceCompanyDocumentName",
                table: "VerificationConfirmRequirement");

            migrationBuilder.DropColumn(
                name: "AnnounceCompanyDocumentSize",
                table: "VerificationConfirmRequirement");

            migrationBuilder.DropColumn(
                name: "AnnounceCompanyDocumentType",
                table: "VerificationConfirmRequirement");

            migrationBuilder.DropColumn(
                name: "AnnounceCompanyDocumentUrl",
                table: "VerificationConfirmRequirement");

            migrationBuilder.DropColumn(
                name: "ConfirmDocumentName",
                table: "VerificationConfirmRequirement");

            migrationBuilder.DropColumn(
                name: "ConfirmDocumentSize",
                table: "VerificationConfirmRequirement");

            migrationBuilder.DropColumn(
                name: "ConfirmDocumentType",
                table: "VerificationConfirmRequirement");

            migrationBuilder.DropColumn(
                name: "ConfirmDocumentUrl",
                table: "VerificationConfirmRequirement");

            migrationBuilder.DropColumn(
                name: "IsUsingAnnounceAgentFile",
                table: "VerificationConfirmRequirement");

            migrationBuilder.DropColumn(
                name: "IsUsingAnnounceCompanyFile",
                table: "VerificationConfirmRequirement");

            migrationBuilder.DropColumn(
                name: "IsUsingConfirmFile",
                table: "VerificationConfirmRequirement");

            migrationBuilder.CreateTable(
                name: "VerificationConfirmDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentSize = table.Column<long>(type: "bigint", nullable: false),
                    VerificationConfirmRequirementId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationConfirmDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerificationConfirmDocument_VerificationConfirmRequirement_VerificationConfirmRequirementId",
                        column: x => x.VerificationConfirmRequirementId,
                        principalTable: "VerificationConfirmRequirement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VerificationConfirmDocument_VerificationConfirmRequirementId",
                table: "VerificationConfirmDocument",
                column: "VerificationConfirmRequirementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerificationConfirmDocument");

            migrationBuilder.AddColumn<string>(
                name: "AnnounceAgentDocumentName",
                table: "VerificationConfirmRequirement",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AnnounceAgentDocumentSize",
                table: "VerificationConfirmRequirement",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "AnnounceAgentDocumentType",
                table: "VerificationConfirmRequirement",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnnounceAgentDocumentUrl",
                table: "VerificationConfirmRequirement",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnnounceCompanyDocumentContent",
                table: "VerificationConfirmRequirement",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnnounceCompanyDocumentName",
                table: "VerificationConfirmRequirement",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AnnounceCompanyDocumentSize",
                table: "VerificationConfirmRequirement",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "AnnounceCompanyDocumentType",
                table: "VerificationConfirmRequirement",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnnounceCompanyDocumentUrl",
                table: "VerificationConfirmRequirement",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmDocumentName",
                table: "VerificationConfirmRequirement",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ConfirmDocumentSize",
                table: "VerificationConfirmRequirement",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmDocumentType",
                table: "VerificationConfirmRequirement",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmDocumentUrl",
                table: "VerificationConfirmRequirement",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsingAnnounceAgentFile",
                table: "VerificationConfirmRequirement",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsingAnnounceCompanyFile",
                table: "VerificationConfirmRequirement",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsingConfirmFile",
                table: "VerificationConfirmRequirement",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
