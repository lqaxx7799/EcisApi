using Microsoft.EntityFrameworkCore.Migrations;

namespace EcisApi.Migrations
{
    public partial class updateDb_verificationCriteria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReviewComment",
                table: "VerificationCriteria",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewResult",
                table: "VerificationCriteria",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewComment",
                table: "VerificationCriteria");

            migrationBuilder.DropColumn(
                name: "ReviewResult",
                table: "VerificationCriteria");
        }
    }
}
