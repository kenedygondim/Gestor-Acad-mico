using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_Acadêmico.Migrations
{
    /// <inheritdoc />
    public partial class ConsertandoPropriedade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Frequencia",
                table: "Notas",
                newName: "FrequenciaDoAluno");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FrequenciaDoAluno",
                table: "Notas",
                newName: "Frequencia");
        }
    }
}
