using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class DocumentsListModel_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Document",
                table: "DocumentLists");

            migrationBuilder.RenameColumn(
                name: "Stamp_Date",
                table: "DocumentLists",
                newName: "Stamp_Duty");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Document_Date",
                table: "DocumentLists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentAmount",
                table: "DocumentLists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentAmount",
                table: "DocumentLists");

            migrationBuilder.RenameColumn(
                name: "Stamp_Duty",
                table: "DocumentLists",
                newName: "Stamp_Date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Document_Date",
                table: "DocumentLists",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Document",
                table: "DocumentLists",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
