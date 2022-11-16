using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class Trust_To_BaseModifierModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Trusts");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Trusts");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Trusts",
                newName: "Modifier");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Trusts",
                newName: "ModificationDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Trusts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatorName",
                table: "Trusts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Trusts");

            migrationBuilder.DropColumn(
                name: "CreatorName",
                table: "Trusts");

            migrationBuilder.RenameColumn(
                name: "Modifier",
                table: "Trusts",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "ModificationDate",
                table: "Trusts",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Trusts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modified",
                table: "Trusts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
