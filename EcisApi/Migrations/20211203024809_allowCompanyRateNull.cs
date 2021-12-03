using Microsoft.EntityFrameworkCore.Migrations;

namespace EcisApi.Migrations
{
    public partial class allowCompanyRateNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "CompanyRate",
                table: "VerificationCriteria",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "CompanyRate",
                table: "VerificationCriteria",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
