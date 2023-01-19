using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class Updated_Notice_Automation_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedDrafts");

            migrationBuilder.DropColumn(
                name: "Forum",
                table: "NoticeAutomations");

            migrationBuilder.DropColumn(
                name: "NoticeTitle",
                table: "NoticeAutomations");

            migrationBuilder.RenameColumn(
                name: "Notice",
                table: "NoticeAutomations",
                newName: "NoticeFormat");

            migrationBuilder.AddColumn<string>(
                name: "FormatName",
                table: "NoticeAutomations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormatName",
                table: "NoticeAutomations");

            migrationBuilder.RenameColumn(
                name: "NoticeFormat",
                table: "NoticeAutomations",
                newName: "Notice");

            migrationBuilder.AddColumn<string>(
                name: "Forum",
                table: "NoticeAutomations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NoticeTitle",
                table: "NoticeAutomations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SavedDrafts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowerCode = table.Column<int>(type: "int", nullable: false),
                    Draft = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedDrafts", x => x.Id);
                });
        }
    }
}
