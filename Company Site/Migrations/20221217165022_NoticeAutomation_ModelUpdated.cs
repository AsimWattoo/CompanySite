using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class NoticeAutomation_ModelUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchSavedDraft",
                table: "NoticeAutomations");

            migrationBuilder.AddColumn<int>(
                name: "BorrowerCode",
                table: "NoticeAutomations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BorrowerCode",
                table: "NoticeAutomations");

            migrationBuilder.AddColumn<string>(
                name: "SearchSavedDraft",
                table: "NoticeAutomations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
