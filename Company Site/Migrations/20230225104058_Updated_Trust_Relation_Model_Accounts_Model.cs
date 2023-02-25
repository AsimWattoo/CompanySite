using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class Updated_Trust_Relation_Model_Accounts_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcquisitonPrice",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AcquistionDate",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Assignor",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "SRIssued",
                table: "Accounts");

            migrationBuilder.AddColumn<double>(
                name: "AcquisitonPrice",
                table: "TrustRelations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AcquistionDate",
                table: "TrustRelations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Assignor",
                table: "TrustRelations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SRIssued",
                table: "TrustRelations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcquisitonPrice",
                table: "TrustRelations");

            migrationBuilder.DropColumn(
                name: "AcquistionDate",
                table: "TrustRelations");

            migrationBuilder.DropColumn(
                name: "Assignor",
                table: "TrustRelations");

            migrationBuilder.DropColumn(
                name: "SRIssued",
                table: "TrustRelations");

            migrationBuilder.AddColumn<double>(
                name: "AcquisitonPrice",
                table: "Accounts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AcquistionDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Assignor",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SRIssued",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
