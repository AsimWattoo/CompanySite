using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class AssetDetails_Sub_Models_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BorrowerCode",
                table: "ValuationDetails",
                newName: "AssetDetailsId");

            migrationBuilder.RenameColumn(
                name: "BorrowerCode",
                table: "AuctionDetails",
                newName: "AssetDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssetDetailsId",
                table: "ValuationDetails",
                newName: "BorrowerCode");

            migrationBuilder.RenameColumn(
                name: "AssetDetailsId",
                table: "AuctionDetails",
                newName: "BorrowerCode");
        }
    }
}
