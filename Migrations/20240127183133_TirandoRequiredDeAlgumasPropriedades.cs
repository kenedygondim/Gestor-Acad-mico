using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_Acadêmico.Migrations
{
    /// <inheritdoc />
    public partial class TirandoRequiredDeAlgumasPropriedades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Disciplinas_DisciplinaId",
                table: "Notas");

            migrationBuilder.DropIndex(
                name: "IX_Notas_DisciplinaId",
                table: "Notas");

            migrationBuilder.AlterColumn<bool>(
                name: "NotasFechadas",
                table: "Notas",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DisciplinaId",
                table: "Notas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AlunoId",
                table: "Notas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_DisciplinaId",
                table: "Notas",
                column: "DisciplinaId",
                unique: true,
                filter: "[DisciplinaId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Disciplinas_DisciplinaId",
                table: "Notas",
                column: "DisciplinaId",
                principalTable: "Disciplinas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Disciplinas_DisciplinaId",
                table: "Notas");

            migrationBuilder.DropIndex(
                name: "IX_Notas_DisciplinaId",
                table: "Notas");

            migrationBuilder.AlterColumn<bool>(
                name: "NotasFechadas",
                table: "Notas",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "DisciplinaId",
                table: "Notas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AlunoId",
                table: "Notas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notas_DisciplinaId",
                table: "Notas",
                column: "DisciplinaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Disciplinas_DisciplinaId",
                table: "Notas",
                column: "DisciplinaId",
                principalTable: "Disciplinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
