using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class ResolutionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResolutionStatusModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowerCode = table.Column<int>(type: "int", nullable: false),
                    Current_Progress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RBI_Reporting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating_Reporting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Case_Update = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Important_information = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Challenges = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recommended_Strategy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Next_steps = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resolution_plan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Restructuring_plan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResolutionStatusModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResolutionStatusModels");
        }
    }
}
