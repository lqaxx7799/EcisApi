using Microsoft.EntityFrameworkCore.Migrations;

namespace EcisApi.Migrations
{
    public partial class updateRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasManagement",
                table: "Role",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasManagement",
                table: "Role");
        }
    }
}
