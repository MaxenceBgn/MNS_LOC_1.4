using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthMVC2.Data.Migrations
{
    public partial class TableMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorMaterial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultMaterial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFunctional = table.Column<bool>(type: "bit", nullable: false),
                    PurchaseDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HaveAnAlimentation = table.Column<bool>(type: "bit", nullable: false),
                    MaterialDocumentation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.MaterialId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materials");
        }
    }
}
