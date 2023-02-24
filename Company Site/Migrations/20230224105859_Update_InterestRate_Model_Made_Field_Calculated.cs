using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class Update_InterestRate_Model_Made_Field_Calculated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RevisedInterestRate",
                table: "InterestRateChangeEntries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "RevisedInterestRate",
                table: "InterestRateChangeEntries",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
