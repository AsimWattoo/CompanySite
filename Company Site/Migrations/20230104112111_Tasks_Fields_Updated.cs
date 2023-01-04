using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class Tasks_Fields_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reminder",
                table: "Tasks",
                newName: "ReminderTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReminderDate",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReminderDate",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "ReminderTime",
                table: "Tasks",
                newName: "Reminder");
        }
    }
}
