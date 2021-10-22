using Microsoft.EntityFrameworkCore.Migrations;

namespace EcisApi.Migrations
{
    public partial class updateDbCompanyReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResourceUrl",
                table: "CompanyReportDocument",
                newName: "DocumentUrl");

            migrationBuilder.RenameColumn(
                name: "ResourceType",
                table: "CompanyReportDocument",
                newName: "DocumentType");

            migrationBuilder.AddColumn<string>(
                name: "DocumentName",
                table: "CompanyReportDocument",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DocumentSize",
                table: "CompanyReportDocument",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "CompanyReport",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentName",
                table: "CompanyReportDocument");

            migrationBuilder.DropColumn(
                name: "DocumentSize",
                table: "CompanyReportDocument");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CompanyReport");

            migrationBuilder.RenameColumn(
                name: "DocumentUrl",
                table: "CompanyReportDocument",
                newName: "ResourceUrl");

            migrationBuilder.RenameColumn(
                name: "DocumentType",
                table: "CompanyReportDocument",
                newName: "ResourceType");
        }
    }
}
