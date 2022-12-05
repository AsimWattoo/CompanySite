using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class Contacts_Model_Added_Again : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
