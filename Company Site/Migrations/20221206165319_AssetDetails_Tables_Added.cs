using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class AssetDetails_Tables_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowerCode = table.Column<int>(type: "int", nullable: false),
                    AssetId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Charge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssetDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FreeHold = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mortgagor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MortgageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Possession = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegalDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Locality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PinCode = table.Column<long>(type: "bigint", nullable: false),
                    East = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    West = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    North = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    South = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeasedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lessor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lessee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceValue = table.Column<long>(type: "bigint", nullable: false),
                    BookValue = table.Column<long>(type: "bigint", nullable: false),
                    NDC_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Encumbrance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Custodian = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityTrustee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MortageDeed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepositTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleDeed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfSecurity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RateOfHolding = table.Column<long>(type: "bigint", nullable: false),
                    ISIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoldingPercentage = table.Column<double>(type: "float", nullable: false),
                    FaceValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DP_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Listed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CouponRate = table.Column<double>(type: "float", nullable: false),
                    DP_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cersai_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Form = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuctionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowerCode = table.Column<int>(type: "int", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuctionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservePrice = table.Column<double>(type: "float", nullable: false),
                    Outcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfBidder = table.Column<int>(type: "int", nullable: false),
                    BuyPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ValuationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowerCode = table.Column<int>(type: "int", nullable: false),
                    Valuer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValuationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Property = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FMV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RSV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DVS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Scrap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuationDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetDetails");

            migrationBuilder.DropTable(
                name: "AuctionDetails");

            migrationBuilder.DropTable(
                name: "ValuationDetails");
        }
    }
}
