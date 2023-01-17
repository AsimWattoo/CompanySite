using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class ResolutionStatus_Updated_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Restructuring_Plan_ModificationDate",
                table: "ResolutionStatusModels",
                newName: "Restructuring_Plan_Modification_Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Restructuring_Plan_Modification_Date",
                table: "ResolutionStatusModels",
                newName: "Restructuring_Plan_ModificationDate");
        }
    }
}
