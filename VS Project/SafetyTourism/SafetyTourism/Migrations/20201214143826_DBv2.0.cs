using Microsoft.EntityFrameworkCore.Migrations;

namespace SafetyTourism.Migrations
{
    public partial class DBv20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Recomendacao",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Recomendacao");
        }
    }
}
