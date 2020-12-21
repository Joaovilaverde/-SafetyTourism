using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OMS_API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Virus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeVirus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Virus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zona",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zona", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZonaId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pais_Zona_ZonaId",
                        column: x => x.ZonaId,
                        principalTable: "Zona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recomendacao",
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
                    table.PrimaryKey("PK_Recomendacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recomendacao_Zona_ZonaId",
                        column: x => x.ZonaId,
                        principalTable: "Zona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Surto",
                columns: table => new
                {
                    SurtoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VirusId = table.Column<long>(type: "bigint", nullable: false),
                    ZonaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataDetecao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surto", x => x.SurtoId);
                    table.ForeignKey(
                        name: "FK_Surto_Virus_VirusId",
                        column: x => x.VirusId,
                        principalTable: "Virus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Surto_Zona_ZonaId",
                        column: x => x.ZonaId,
                        principalTable: "Zona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pais_ZonaId",
                table: "Pais",
                column: "ZonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Recomendacao_ZonaId",
                table: "Recomendacao",
                column: "ZonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Surto_VirusId",
                table: "Surto",
                column: "VirusId");

            migrationBuilder.CreateIndex(
                name: "IX_Surto_ZonaId",
                table: "Surto",
                column: "ZonaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pais");

            migrationBuilder.DropTable(
                name: "Recomendacao");

            migrationBuilder.DropTable(
                name: "Surto");

            migrationBuilder.DropTable(
                name: "Virus");

            migrationBuilder.DropTable(
                name: "Zona");
        }
    }
}
