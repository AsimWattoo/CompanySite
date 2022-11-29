using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class NCLTNet_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClaimsAndShares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowerCode = table.Column<int>(type: "int", nullable: false),
                    LenderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimSubmitted = table.Column<int>(type: "int", nullable: false),
                    Admitted = table.Column<int>(type: "int", nullable: false),
                    Share = table.Column<double>(type: "float", nullable: false),
                    VotingShare = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimsAndShares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Landline_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Service1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Service2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Empanelled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number_Assignment = table.Column<long>(type: "bigint", nullable: false),
                    KYC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bill_Number = table.Column<long>(type: "bigint", nullable: false),
                    Bill_Amount = table.Column<long>(type: "bigint", nullable: false),
                    Bill_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Payment_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Payment_Amount = table.Column<long>(type: "bigint", nullable: false),
                    Amount_OS = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NcltNets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    case_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BorrowerCode = table.Column<int>(type: "int", nullable: false),
                    insolvery_commencement_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IPE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Applicant_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    court = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Asset_Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    liquidator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Liquidation_Value = table.Column<double>(type: "float", nullable: false),
                    current_stage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LiquidationDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompaniesAct = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NcltNets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RADetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowerCode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanValue = table.Column<int>(type: "int", nullable: false),
                    OurShare = table.Column<int>(type: "int", nullable: false),
                    PaymentTimeline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Scoring = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RADetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Timelines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowerCode = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Particulars = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timelines", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimsAndShares");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "NcltNets");

            migrationBuilder.DropTable(
                name: "RADetails");

            migrationBuilder.DropTable(
                name: "Timelines");
        }
    }
}
