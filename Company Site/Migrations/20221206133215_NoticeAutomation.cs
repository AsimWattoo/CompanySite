using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class NoticeAutomation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NoticeAutomations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoticeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoticeTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Forum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchSavedDraft = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoticeAutomations", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoticeAutomations");

            migrationBuilder.DropTable(
                name: "SavedDrafts");
        }
    }
}
