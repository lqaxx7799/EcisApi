using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcisApi.Migrations
{
    public partial class processNewField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "VerificationProcess",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmitDeadline",
                table: "VerificationProcess",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedAt",
                table: "VerificationProcess",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "VerificationProcess");

            migrationBuilder.DropColumn(
                name: "SubmitDeadline",
                table: "VerificationProcess");

            migrationBuilder.DropColumn(
                name: "SubmittedAt",
                table: "VerificationProcess");
        }
    }
}
