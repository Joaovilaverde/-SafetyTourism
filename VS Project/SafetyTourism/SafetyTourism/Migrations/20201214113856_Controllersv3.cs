using Microsoft.EntityFrameworkCore.Migrations;

namespace SafetyTourism.Migrations
{
    public partial class Controllersv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_OfficeAssignment_RecomendacaoId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Department_DoencaId",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Student_DestinoId",
                table: "Instructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfficeAssignment",
                table: "OfficeAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructor",
                table: "Instructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollment",
                table: "Enrollment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Department",
                table: "Department");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Destino");

            migrationBuilder.RenameTable(
                name: "OfficeAssignment",
                newName: "Recomendacao");

            migrationBuilder.RenameTable(
                name: "Instructor",
                newName: "AfectadoPor");

            migrationBuilder.RenameTable(
                name: "Enrollment",
                newName: "Utilizador");

            migrationBuilder.RenameTable(
                name: "Department",
                newName: "Doenca");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Funcionario");

            migrationBuilder.RenameIndex(
                name: "IX_Instructor_DoencaId",
                table: "AfectadoPor",
                newName: "IX_AfectadoPor_DoencaId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructor_DestinoId",
                table: "AfectadoPor",
                newName: "IX_AfectadoPor_DestinoId");

            migrationBuilder.RenameIndex(
                name: "IX_Department_RecomendacaoId",
                table: "Doenca",
                newName: "IX_Doenca_RecomendacaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Destino",
                table: "Destino",
                column: "DestinoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recomendacao",
                table: "Recomendacao",
                column: "RecomendacaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AfectadoPor",
                table: "AfectadoPor",
                column: "AfectadoPorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utilizador",
                table: "Utilizador",
                column: "UtilizadorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doenca",
                table: "Doenca",
                column: "DoencaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcionario",
                table: "Funcionario",
                column: "FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AfectadoPor_Destino_DestinoId",
                table: "AfectadoPor",
                column: "DestinoId",
                principalTable: "Destino",
                principalColumn: "DestinoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AfectadoPor_Doenca_DoencaId",
                table: "AfectadoPor",
                column: "DoencaId",
                principalTable: "Doenca",
                principalColumn: "DoencaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doenca_Recomendacao_RecomendacaoId",
                table: "Doenca",
                column: "RecomendacaoId",
                principalTable: "Recomendacao",
                principalColumn: "RecomendacaoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AfectadoPor_Destino_DestinoId",
                table: "AfectadoPor");

            migrationBuilder.DropForeignKey(
                name: "FK_AfectadoPor_Doenca_DoencaId",
                table: "AfectadoPor");

            migrationBuilder.DropForeignKey(
                name: "FK_Doenca_Recomendacao_RecomendacaoId",
                table: "Doenca");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Utilizador",
                table: "Utilizador");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recomendacao",
                table: "Recomendacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Funcionario",
                table: "Funcionario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doenca",
                table: "Doenca");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Destino",
                table: "Destino");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AfectadoPor",
                table: "AfectadoPor");

            migrationBuilder.RenameTable(
                name: "Utilizador",
                newName: "Enrollment");

            migrationBuilder.RenameTable(
                name: "Recomendacao",
                newName: "OfficeAssignment");

            migrationBuilder.RenameTable(
                name: "Funcionario",
                newName: "Course");

            migrationBuilder.RenameTable(
                name: "Doenca",
                newName: "Department");

            migrationBuilder.RenameTable(
                name: "Destino",
                newName: "Student");

            migrationBuilder.RenameTable(
                name: "AfectadoPor",
                newName: "Instructor");

            migrationBuilder.RenameIndex(
                name: "IX_Doenca_RecomendacaoId",
                table: "Department",
                newName: "IX_Department_RecomendacaoId");

            migrationBuilder.RenameIndex(
                name: "IX_AfectadoPor_DoencaId",
                table: "Instructor",
                newName: "IX_Instructor_DoencaId");

            migrationBuilder.RenameIndex(
                name: "IX_AfectadoPor_DestinoId",
                table: "Instructor",
                newName: "IX_Instructor_DestinoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollment",
                table: "Enrollment",
                column: "UtilizadorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfficeAssignment",
                table: "OfficeAssignment",
                column: "RecomendacaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "FuncionarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Department",
                table: "Department",
                column: "DoencaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "DestinoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructor",
                table: "Instructor",
                column: "AfectadoPorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_OfficeAssignment_RecomendacaoId",
                table: "Department",
                column: "RecomendacaoId",
                principalTable: "OfficeAssignment",
                principalColumn: "RecomendacaoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Department_DoencaId",
                table: "Instructor",
                column: "DoencaId",
                principalTable: "Department",
                principalColumn: "DoencaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Student_DestinoId",
                table: "Instructor",
                column: "DestinoId",
                principalTable: "Student",
                principalColumn: "DestinoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
