using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class Collections_Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustRelations_Trusts_TrustId",
                table: "TrustRelations");

            migrationBuilder.DropTable(
                name: "CollectionSubEntries");

            migrationBuilder.DropIndex(
                name: "IX_TrustRelations_TrustId",
                table: "TrustRelations");

            migrationBuilder.DropColumn(
                name: "TrustId",
                table: "TrustRelations");

            migrationBuilder.DropColumn(
                name: "TrustCode",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TrustName",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Collections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Collections");

            migrationBuilder.AddColumn<int>(
                name: "TrustId",
                table: "TrustRelations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrustCode",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrustName",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CollectionSubEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    BorrowerCode = table.Column<int>(type: "int", nullable: false),
                    CollectionEntryId = table.Column<int>(type: "int", nullable: false),
                    HolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Share = table.Column<float>(type: "real", nullable: false),
                    TrustCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrustName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionSubEntries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrustRelations_TrustId",
                table: "TrustRelations",
                column: "TrustId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustRelations_Trusts_TrustId",
                table: "TrustRelations",
                column: "TrustId",
                principalTable: "Trusts",
                principalColumn: "Id");
        }
    }
}
