using Microsoft.EntityFrameworkCore.Migrations;

namespace EcisApi.Migrations
{
    public partial class updateDb_confirm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VerificationCriteriaId",
                table: "VerificationConfirmRequirement",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VerificationConfirmRequirement_VerificationCriteriaId",
                table: "VerificationConfirmRequirement",
                column: "VerificationCriteriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationConfirmRequirement_VerificationCriteria_VerificationCriteriaId",
                table: "VerificationConfirmRequirement",
                column: "VerificationCriteriaId",
                principalTable: "VerificationCriteria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerificationConfirmRequirement_VerificationCriteria_VerificationCriteriaId",
                table: "VerificationConfirmRequirement");

            migrationBuilder.DropIndex(
                name: "IX_VerificationConfirmRequirement_VerificationCriteriaId",
                table: "VerificationConfirmRequirement");

            migrationBuilder.DropColumn(
                name: "VerificationCriteriaId",
                table: "VerificationConfirmRequirement");
        }
    }
}
