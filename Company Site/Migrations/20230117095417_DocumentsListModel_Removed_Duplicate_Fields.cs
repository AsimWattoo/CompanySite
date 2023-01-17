using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class DocumentsListModel_Removed_Duplicate_Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Doc_type",
                table: "DocumentLists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Doc_type",
                table: "DocumentLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
