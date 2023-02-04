using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class Finance_Models_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Proportionately",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Proportionately",
                table: "Collections");

            migrationBuilder.AddColumn<string>(
                name: "GSTNumber",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GSTNumber",
                table: "Expenses");

            migrationBuilder.AddColumn<bool>(
                name: "Proportionately",
                table: "Expenses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Proportionately",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
