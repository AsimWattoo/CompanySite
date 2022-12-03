using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class Account_Net_Update_Borrower_Code : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BorrowerCode",
                table: "AccountNets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BorrowerCode",
                table: "AccountNets");
        }
    }
}
