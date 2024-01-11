using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_Acadêmico.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingSomeProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryCourseId",
                table: "Courses",
                newName: "CategoryCourse");

            migrationBuilder.AlterColumn<decimal>(
                name: "Hours",
                table: "Subjects",
                type: "decimal(8,1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "Hours",
                table: "Courses",
                type: "decimal(8,1)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hours",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "CategoryCourse",
                table: "Courses",
                newName: "CategoryCourseId");

            migrationBuilder.AlterColumn<int>(
                name: "Hours",
                table: "Subjects",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,1)");
        }
    }
}
