using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_Acadêmico.Migrations
{
    /// <inheritdoc />
    public partial class NewPropertiesCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Openings",
                table: "Courses",
                newName: "SemesterDurationInWeeks");

            migrationBuilder.AddColumn<int>(
                name: "OpeningsPerSemester",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpeningsPerSemester",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "SemesterDurationInWeeks",
                table: "Courses",
                newName: "Openings");
        }
    }
}
