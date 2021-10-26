using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcisApi.Migrations
{
    public partial class initV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTypeModification_ModificationType_ModificationTypeId",
                table: "CompanyTypeModification");

            migrationBuilder.DropForeignKey(
                name: "FK_VerificationCriteria_Criteria_CriteriaId",
                table: "VerificationCriteria");

            migrationBuilder.DropTable(
                name: "ModificationType");

            migrationBuilder.DropIndex(
                name: "IX_CompanyTypeModification_ModificationTypeId",
                table: "CompanyTypeModification");

            migrationBuilder.DropColumn(
                name: "ModificationTypeId",
                table: "CompanyTypeModification");

            migrationBuilder.RenameColumn(
                name: "CriteriaId",
                table: "VerificationCriteria",
                newName: "CriteriaDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_VerificationCriteria_CriteriaId",
                table: "VerificationCriteria",
                newName: "IX_VerificationCriteria_CriteriaDetailId");

            migrationBuilder.AddColumn<string>(
                name: "CompanyOpinion",
                table: "VerificationCriteria",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CompanyRate",
                table: "VerificationCriteria",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "AnnouncedAt",
                table: "CompanyTypeModification",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CriteriaDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriteriaDetailName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CriteriaId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriteriaDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriteriaDetail_Criteria_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "Criteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaDetail_CriteriaId",
                table: "CriteriaDetail",
                column: "CriteriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationCriteria_CriteriaDetail_CriteriaDetailId",
                table: "VerificationCriteria",
                column: "CriteriaDetailId",
                principalTable: "CriteriaDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerificationCriteria_CriteriaDetail_CriteriaDetailId",
                table: "VerificationCriteria");

            migrationBuilder.DropTable(
                name: "CriteriaDetail");

            migrationBuilder.DropColumn(
                name: "CompanyOpinion",
                table: "VerificationCriteria");

            migrationBuilder.DropColumn(
                name: "CompanyRate",
                table: "VerificationCriteria");

            migrationBuilder.DropColumn(
                name: "AnnouncedAt",
                table: "CompanyTypeModification");

            migrationBuilder.RenameColumn(
                name: "CriteriaDetailId",
                table: "VerificationCriteria",
                newName: "CriteriaId");

            migrationBuilder.RenameIndex(
                name: "IX_VerificationCriteria_CriteriaDetailId",
                table: "VerificationCriteria",
                newName: "IX_VerificationCriteria_CriteriaId");

            migrationBuilder.AddColumn<int>(
                name: "ModificationTypeId",
                table: "CompanyTypeModification",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ModificationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificationType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTypeModification_ModificationTypeId",
                table: "CompanyTypeModification",
                column: "ModificationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTypeModification_ModificationType_ModificationTypeId",
                table: "CompanyTypeModification",
                column: "ModificationTypeId",
                principalTable: "ModificationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationCriteria_Criteria_CriteriaId",
                table: "VerificationCriteria",
                column: "CriteriaId",
                principalTable: "Criteria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
