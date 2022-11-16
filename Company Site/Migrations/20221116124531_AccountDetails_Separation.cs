using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class AccountDetails_Separation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "TrustRelations");

            migrationBuilder.DropColumn(
                name: "Acquisition_Date",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Background",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Bank",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Branch",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Case_Exit",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Case_Exit_Date",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Company_Name",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Constitution",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CreatorName",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "DD_Date",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "DD_Firm",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "DD_Official",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "DateOfDefault",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "FinalDate",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "GroupHead",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Industry",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Last_Payment",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "MeasuresOfReconstruction",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Modifier",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Officers",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Plant_Location",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Products",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ResolutionStatus",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ResolutionStrategy",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Statutory_Dues",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "Reason_NPA",
                table: "Accounts",
                newName: "TrustName");

            migrationBuilder.RenameColumn(
                name: "Operating_Status",
                table: "Accounts",
                newName: "Company");

            migrationBuilder.RenameColumn(
                name: "NPA_Date",
                table: "Accounts",
                newName: "AcquistionDate");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Accounts",
                newName: "Assignor");

            migrationBuilder.AddColumn<double>(
                name: "AcquisitonPrice",
                table: "Accounts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "SRIssued",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AccountDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrustCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BorrowerCode = table.Column<int>(type: "int", nullable: false),
                    Officers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupHead = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plant_Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfDefault = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResolutionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Products = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last_Payment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResolutionStrategy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Constitution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Operating_Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NPA_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Acquisition_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statutory_Dues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasuresOfReconstruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DD_Official = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DD_Firm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DD_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Background = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason_NPA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Case_Exit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Case_Exit_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountDetails");

            migrationBuilder.DropColumn(
                name: "AcquisitonPrice",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "SRIssued",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "TrustName",
                table: "Accounts",
                newName: "Reason_NPA");

            migrationBuilder.RenameColumn(
                name: "Company",
                table: "Accounts",
                newName: "Operating_Status");

            migrationBuilder.RenameColumn(
                name: "Assignor",
                table: "Accounts",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "AcquistionDate",
                table: "Accounts",
                newName: "NPA_Date");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "TrustRelations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Acquisition_Date",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Bank",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Branch",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Capacity",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Case_Exit",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Case_Exit_Date",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Company_Name",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Constitution",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatorName",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DD_Date",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DD_Firm",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DD_Official",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfDefault",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FinalDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "GroupHead",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Industry",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Last_Payment",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasuresOfReconstruction",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Modifier",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Officers",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Plant_Location",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Products",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResolutionStatus",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResolutionStrategy",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Statutory_Dues",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
