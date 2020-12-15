using Microsoft.EntityFrameworkCore.Migrations;

namespace SafetyTourism.Migrations
{
    public partial class EliminarRecomendacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doenca_Recomendacao_RecomendacaoId",
                table: "Doenca");

            migrationBuilder.DropTable(
                name: "Recomendacao");

            migrationBuilder.DropIndex(
                name: "IX_Doenca_RecomendacaoId",
                table: "Doenca");

            migrationBuilder.DropColumn(
                name: "RecomendacaoId",
                table: "Doenca");

            migrationBuilder.AddColumn<string>(
                name: "Recomendacao",
                table: "Doenca",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recomendacao",
                table: "Doenca");

            migrationBuilder.AddColumn<int>(
                name: "RecomendacaoId",
                table: "Doenca",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Recomendacao",
                columns: table => new
                {
                    RecomendacaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Conteudo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recomendacao", x => x.RecomendacaoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doenca_RecomendacaoId",
                table: "Doenca",
                column: "RecomendacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doenca_Recomendacao_RecomendacaoId",
                table: "Doenca",
                column: "RecomendacaoId",
                principalTable: "Recomendacao",
                principalColumn: "RecomendacaoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
