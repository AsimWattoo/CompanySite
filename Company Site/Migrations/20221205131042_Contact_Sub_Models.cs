using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class Contact_Sub_Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessGrants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessGivenBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessGrants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactAccountDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<int>(type: "int", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IFSCI_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Additional = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactAccountDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactPaymentHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillNo = table.Column<int>(type: "int", nullable: false),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillAmount = table.Column<double>(type: "float", nullable: false),
                    BillDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentAmount = table.Column<double>(type: "float", nullable: false),
                    AmountO_S = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPaymentHistories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessGrants");

            migrationBuilder.DropTable(
                name: "ContactAccountDetails");

            migrationBuilder.DropTable(
                name: "ContactPaymentHistories");
        }
    }
}
