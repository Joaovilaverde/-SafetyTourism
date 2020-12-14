using Microsoft.EntityFrameworkCore.Migrations;

namespace SafetyTourism.Migrations
{
    public partial class Controllersv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Afectados_Destinos_DestinoId",
                table: "Afectados");

            migrationBuilder.DropForeignKey(
                name: "FK_Afectados_Doencas_DoencaId",
                table: "Afectados");

            migrationBuilder.DropForeignKey(
                name: "FK_Doencas_Recomendacoes_RecomendacaoId",
                table: "Doencas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Utilizadores",
                table: "Utilizadores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recomendacoes",
                table: "Recomendacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Funcionarios",
                table: "Funcionarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doencas",
                table: "Doencas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Destinos",
                table: "Destinos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Afectados",
                table: "Afectados");

            migrationBuilder.RenameTable(
                name: "Utilizadores",
                newName: "Enrollment");

            migrationBuilder.RenameTable(
                name: "Recomendacoes",
                newName: "OfficeAssignment");

            migrationBuilder.RenameTable(
                name: "Funcionarios",
                newName: "Course");

            migrationBuilder.RenameTable(
                name: "Doencas",
                newName: "Department");

            migrationBuilder.RenameTable(
                name: "Destinos",
                newName: "Student");

            migrationBuilder.RenameTable(
                name: "Afectados",
                newName: "Instructor");

            migrationBuilder.RenameIndex(
                name: "IX_Doencas_RecomendacaoId",
                table: "Department",
                newName: "IX_Department_RecomendacaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Afectados_DoencaId",
                table: "Instructor",
                newName: "IX_Instructor_DoencaId");

            migrationBuilder.RenameIndex(
                name: "IX_Afectados_DestinoId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "Destinos");

            migrationBuilder.RenameTable(
                name: "OfficeAssignment",
                newName: "Recomendacoes");

            migrationBuilder.RenameTable(
                name: "Instructor",
                newName: "Afectados");

            migrationBuilder.RenameTable(
                name: "Enrollment",
                newName: "Utilizadores");

            migrationBuilder.RenameTable(
                name: "Department",
                newName: "Doencas");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Funcionarios");

            migrationBuilder.RenameIndex(
                name: "IX_Instructor_DoencaId",
                table: "Afectados",
                newName: "IX_Afectados_DoencaId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructor_DestinoId",
                table: "Afectados",
                newName: "IX_Afectados_DestinoId");

            migrationBuilder.RenameIndex(
                name: "IX_Department_RecomendacaoId",
                table: "Doencas",
                newName: "IX_Doencas_RecomendacaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Destinos",
                table: "Destinos",
                column: "DestinoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recomendacoes",
                table: "Recomendacoes",
                column: "RecomendacaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Afectados",
                table: "Afectados",
                column: "AfectadoPorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utilizadores",
                table: "Utilizadores",
                column: "UtilizadorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doencas",
                table: "Doencas",
                column: "DoencaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcionarios",
                table: "Funcionarios",
                column: "FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Afectados_Destinos_DestinoId",
                table: "Afectados",
                column: "DestinoId",
                principalTable: "Destinos",
                principalColumn: "DestinoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Afectados_Doencas_DoencaId",
                table: "Afectados",
                column: "DoencaId",
                principalTable: "Doencas",
                principalColumn: "DoencaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doencas_Recomendacoes_RecomendacaoId",
                table: "Doencas",
                column: "RecomendacaoId",
                principalTable: "Recomendacoes",
                principalColumn: "RecomendacaoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
