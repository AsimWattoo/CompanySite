using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class AccountNetModel_Added_Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Advance",
                table: "AccountNets");

            migrationBuilder.DropColumn(
                name: "BillNumber",
                table: "AccountNets");

            migrationBuilder.DropColumn(
                name: "CourtFees",
                table: "AccountNets");

            migrationBuilder.DropColumn(
                name: "FeePerHearing",
                table: "AccountNets");

            migrationBuilder.DropColumn(
                name: "TotalFeesQuotation",
                table: "AccountNets");

            migrationBuilder.AddColumn<int>(
                name: "DaysToLimitation",
                table: "AccountNets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastDebtAcknowledgement",
                table: "AccountNets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysToLimitation",
                table: "AccountNets");

            migrationBuilder.DropColumn(
                name: "LastDebtAcknowledgement",
                table: "AccountNets");

            migrationBuilder.AddColumn<double>(
                name: "Advance",
                table: "AccountNets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BillNumber",
                table: "AccountNets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CourtFees",
                table: "AccountNets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FeePerHearing",
                table: "AccountNets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalFeesQuotation",
                table: "AccountNets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
