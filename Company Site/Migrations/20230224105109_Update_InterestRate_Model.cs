using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class Update_InterestRate_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BaseInterestRate",
                table: "InterestRateChangeEntries",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Spread",
                table: "InterestRateChangeEntries",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseInterestRate",
                table: "InterestRateChangeEntries");

            migrationBuilder.DropColumn(
                name: "Spread",
                table: "InterestRateChangeEntries");
        }
    }
}
