using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class MemoModel_Added_Field_GST : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AddColumn<int>(
				name: "GSTNumber",
				table: "Memos",
				type: "nvarchar(max)",
				nullable: true,
				defaultValue: "");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropColumn(
				name: "GSTNumber",
				table: "Memos");
		}
    }
}
