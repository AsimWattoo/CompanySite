using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class FinancialData_Model_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EBDITA",
                table: "FinancialDatas");

            migrationBuilder.DropColumn(
                name: "Networth",
                table: "FinancialDatas");

            migrationBuilder.DropColumn(
                name: "Profit_Loss",
                table: "FinancialDatas");

            migrationBuilder.DropColumn(
                name: "Total_Assets",
                table: "FinancialDatas");

            migrationBuilder.DropColumn(
                name: "Total_Expenses",
                table: "FinancialDatas");

            migrationBuilder.DropColumn(
                name: "Total_liabilities",
                table: "FinancialDatas");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "FinancialDatas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Total_Non_Current_Assets",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Total_Current_Assets",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Tax",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "STB",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Revenue",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Reserves",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Other_Expenses",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Other_Current_Liabilities",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Operating_and_Direct_Expenses",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "LTB",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Fixed_Assets",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Finance_Costs",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Exceptional_items",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Equity",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Employee_Benefit_Expenses",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "EBDITA_per",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Depreciation_And_Amortisation_Expenses",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Changes_in_inventries",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BorrowerCode",
                table: "FinancialDatas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Expenses",
                table: "FinancialDatas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BorrowerCode",
                table: "FinancialDatas");

            migrationBuilder.DropColumn(
                name: "Expenses",
                table: "FinancialDatas");

            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "FinancialDatas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "Total_Non_Current_Assets",
                table: "FinancialDatas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "Total_Current_Assets",
                table: "FinancialDatas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "Tax",
                table: "FinancialDatas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "STB",
                table: "FinancialDatas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "Revenue",
                table: "FinancialDatas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "Reserves",
                table: "FinancialDatas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "Other_Expenses",
                table: "FinancialDatas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "Other_Current_Liabilities",
                table: "FinancialDatas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "Operating_and_Direct_Expenses",
                table: "FinancialDatas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "LTB",
                table: "FinancialDatas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "Fixed_Assets",
                table: "FinancialDatas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "Finance_Costs",
                table: "FinancialDatas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "Exceptional_items",
                table: "FinancialDatas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "Equity",
                table: "FinancialDatas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "Employee_Benefit_Expenses",
                table: "FinancialDatas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "EBDITA_per",
                table: "FinancialDatas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "Depreciation_And_Amortisation_Expenses",
                table: "FinancialDatas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "Changes_in_inventries",
                table: "FinancialDatas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "EBDITA",
                table: "FinancialDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Networth",
                table: "FinancialDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profit_Loss",
                table: "FinancialDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Total_Assets",
                table: "FinancialDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Total_Expenses",
                table: "FinancialDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Total_liabilities",
                table: "FinancialDatas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
