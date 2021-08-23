using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcisApi.Migrations
{
    public partial class addVerificationCriteria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerificationDocument_Criteria_CriteriaId",
                table: "VerificationDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_VerificationDocument_VerificationProcess_VerificationProcessId",
                table: "VerificationDocument");

            migrationBuilder.DropIndex(
                name: "IX_VerificationDocument_CriteriaId",
                table: "VerificationDocument");

            migrationBuilder.DropColumn(
                name: "CriteriaId",
                table: "VerificationDocument");

            migrationBuilder.RenameColumn(
                name: "VerificationProcessId",
                table: "VerificationDocument",
                newName: "VerificationCriteriaId");

            migrationBuilder.RenameIndex(
                name: "IX_VerificationDocument_VerificationProcessId",
                table: "VerificationDocument",
                newName: "IX_VerificationDocument_VerificationCriteriaId");

            migrationBuilder.CreateTable(
                name: "VerificationCriteria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovedStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerificationProcessId = table.Column<int>(type: "int", nullable: true),
                    CriteriaId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerificationCriteria_Criteria_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "Criteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VerificationCriteria_VerificationProcess_VerificationProcessId",
                        column: x => x.VerificationProcessId,
                        principalTable: "VerificationProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VerificationCriteria_CriteriaId",
                table: "VerificationCriteria",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationCriteria_VerificationProcessId",
                table: "VerificationCriteria",
                column: "VerificationProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationDocument_VerificationCriteria_VerificationCriteriaId",
                table: "VerificationDocument",
                column: "VerificationCriteriaId",
                principalTable: "VerificationCriteria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerificationDocument_VerificationCriteria_VerificationCriteriaId",
                table: "VerificationDocument");

            migrationBuilder.DropTable(
                name: "VerificationCriteria");

            migrationBuilder.RenameColumn(
                name: "VerificationCriteriaId",
                table: "VerificationDocument",
                newName: "VerificationProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_VerificationDocument_VerificationCriteriaId",
                table: "VerificationDocument",
                newName: "IX_VerificationDocument_VerificationProcessId");

            migrationBuilder.AddColumn<int>(
                name: "CriteriaId",
                table: "VerificationDocument",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VerificationDocument_CriteriaId",
                table: "VerificationDocument",
                column: "CriteriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationDocument_Criteria_CriteriaId",
                table: "VerificationDocument",
                column: "CriteriaId",
                principalTable: "Criteria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationDocument_VerificationProcess_VerificationProcessId",
                table: "VerificationDocument",
                column: "VerificationProcessId",
                principalTable: "VerificationProcess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
