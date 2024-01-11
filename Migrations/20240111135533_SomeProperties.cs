using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_Acadêmico.Migrations
{
    /// <inheritdoc />
    public partial class SomeProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OpeningsPerSemester",
                table: "Courses",
                newName: "OpeningsLastSemester");

            migrationBuilder.AddColumn<string>(
                name: "Mode",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OpeningsFirstSemester",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "OpeningsFirstSemester",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "OpeningsLastSemester",
                table: "Courses",
                newName: "OpeningsPerSemester");
        }
    }
}
