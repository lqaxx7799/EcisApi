using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcisApi.Migrations
{
    public partial class updateDB_3009 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedAt",
                table: "VerificationProcess",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "VerificationProcess",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "VerificationProcess",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modification",
                table: "CompanyTypeModification",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishedAt",
                table: "VerificationProcess");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "VerificationProcess");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "VerificationProcess");

            migrationBuilder.DropColumn(
                name: "Modification",
                table: "CompanyTypeModification");
        }
    }
}
