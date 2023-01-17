using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class ResolutionStatus_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Case_Update_Modification_Date",
                table: "ResolutionStatusModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Case_Update_Modified_By",
                table: "ResolutionStatusModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Challenges_Modification_Date",
                table: "ResolutionStatusModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Challenges_Modified_By",
                table: "ResolutionStatusModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Current_Progress_Modification_Date",
                table: "ResolutionStatusModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Current_Progress_Modified_By",
                table: "ResolutionStatusModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Important_Information_Modification_Date",
                table: "ResolutionStatusModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Important_Information_Modified_By",
                table: "ResolutionStatusModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Next_Steps_Modification_Date",
                table: "ResolutionStatusModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Next_Steps_Modified_By",
                table: "ResolutionStatusModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RBI_Reporting_Modification_Date",
                table: "ResolutionStatusModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RBI_Reporting_Modified_By",
                table: "ResolutionStatusModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Rating_Reporting_Modification_Date",
                table: "ResolutionStatusModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Rating_Reporting_Modified_By",
                table: "ResolutionStatusModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Recommended_Strategy_Modification_Date",
                table: "ResolutionStatusModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Recommended_Strategy_Modified_By",
                table: "ResolutionStatusModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Resolution_Plan_Modification_Date",
                table: "ResolutionStatusModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Resolution_Plan_Modified_By",
                table: "ResolutionStatusModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Restructuring_Plan_ModificationDate",
                table: "ResolutionStatusModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Restructuring_Plan_Modified_By",
                table: "ResolutionStatusModels",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Case_Update_Modification_Date",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "Case_Update_Modified_By",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "Challenges_Modification_Date",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "Challenges_Modified_By",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "Current_Progress_Modification_Date",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "Current_Progress_Modified_By",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "Important_Information_Modification_Date",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "Important_Information_Modified_By",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "Next_Steps_Modification_Date",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "Next_Steps_Modified_By",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "RBI_Reporting_Modification_Date",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "RBI_Reporting_Modified_By",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "Rating_Reporting_Modification_Date",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "Rating_Reporting_Modified_By",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "Recommended_Strategy_Modification_Date",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "Recommended_Strategy_Modified_By",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "Resolution_Plan_Modification_Date",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "Resolution_Plan_Modified_By",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "Restructuring_Plan_ModificationDate",
                table: "ResolutionStatusModels");

            migrationBuilder.DropColumn(
                name: "Restructuring_Plan_Modified_By",
                table: "ResolutionStatusModels");
        }
    }
}
