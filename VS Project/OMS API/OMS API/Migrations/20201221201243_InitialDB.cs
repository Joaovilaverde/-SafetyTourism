using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OMS_API.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Virus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Virus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zonas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zonas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZonaId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paises_Zonas_ZonaId",
                        column: x => x.ZonaId,
                        principalTable: "Zonas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recomendacoes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZonaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Validade = table.Column<long>(type: "bigint", nullable: false),
                    Informacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recomendacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recomendacoes_Zonas_ZonaId",
                        column: x => x.ZonaId,
                        principalTable: "Zonas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Surtos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VirusId = table.Column<long>(type: "bigint", nullable: false),
                    ZonaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataDetecao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Surtos_Virus_VirusId",
                        column: x => x.VirusId,
                        principalTable: "Virus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Surtos_Zonas_ZonaId",
                        column: x => x.ZonaId,
                        principalTable: "Zonas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Paises_ZonaId",
                table: "Paises",
                column: "ZonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Recomendacoes_ZonaId",
                table: "Recomendacoes",
                column: "ZonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Surtos_VirusId",
                table: "Surtos",
                column: "VirusId");

            migrationBuilder.CreateIndex(
                name: "IX_Surtos_ZonaId",
                table: "Surtos",
                column: "ZonaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paises");

            migrationBuilder.DropTable(
                name: "Recomendacoes");

            migrationBuilder.DropTable(
                name: "Surtos");

            migrationBuilder.DropTable(
                name: "Virus");

            migrationBuilder.DropTable(
                name: "Zonas");
        }
    }
}
