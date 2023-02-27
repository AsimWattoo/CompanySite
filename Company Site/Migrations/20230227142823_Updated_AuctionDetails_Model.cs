using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class Updated_AuctionDetails_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BidDocumentDate",
                table: "AuctionDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "AuctionDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LetterOfConformationDate",
                table: "AuctionDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PastingDate",
                table: "AuctionDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SaleIntimationNoticeDate",
                table: "AuctionDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SuccessBidder",
                table: "AuctionDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BidDocumentDate",
                table: "AuctionDetails");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "AuctionDetails");

            migrationBuilder.DropColumn(
                name: "LetterOfConformationDate",
                table: "AuctionDetails");

            migrationBuilder.DropColumn(
                name: "PastingDate",
                table: "AuctionDetails");

            migrationBuilder.DropColumn(
                name: "SaleIntimationNoticeDate",
                table: "AuctionDetails");

            migrationBuilder.DropColumn(
                name: "SuccessBidder",
                table: "AuctionDetails");
        }
    }
}
