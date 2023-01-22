using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class MemoMaster_Updated_CaseName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CaseName",
                table: "Memos",
                newName: "Service");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Service",
                table: "Memos",
                newName: "CaseName");
        }
    }
}
