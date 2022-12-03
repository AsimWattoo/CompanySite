using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class Account_Net_Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountNets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CaseTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Forum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JudgeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PetitionerMultiLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Petitioner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalFeesQuotation = table.Column<double>(type: "float", nullable: false),
                    BilledAmount = table.Column<double>(type: "float", nullable: false),
                    CourtFees = table.Column<double>(type: "float", nullable: false),
                    CaseDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Development = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinalOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Court = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimAmount = table.Column<double>(type: "float", nullable: false),
                    Bench = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Respondent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Respondent_adv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Advance = table.Column<double>(type: "float", nullable: false),
                    AmountPaid = table.Column<double>(type: "float", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNRNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNumber = table.Column<int>(type: "int", nullable: false),
                    NextDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentStage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FeePerHearing = table.Column<double>(type: "float", nullable: false),
                    OSAmount = table.Column<double>(type: "float", nullable: false),
                    BillNumber = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountNets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountNets");
        }
    }
}
