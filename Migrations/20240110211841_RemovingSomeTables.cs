using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_Acadêmico.Migrations
{
    /// <inheritdoc />
    public partial class RemovingSomeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CategoriesCourse_CategoryCourseId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Turns_TurnId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "CategoriesCourse");

            migrationBuilder.DropTable(
                name: "Turns");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CategoryCourseId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TurnId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TurnId",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryCourseId",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Turn",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Turn",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryCourseId",
                table: "Courses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "TurnId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoriesCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesCourse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurnCourse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turns", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryCourseId",
                table: "Courses",
                column: "CategoryCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TurnId",
                table: "Courses",
                column: "TurnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CategoriesCourse_CategoryCourseId",
                table: "Courses",
                column: "CategoryCourseId",
                principalTable: "CategoriesCourse",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Turns_TurnId",
                table: "Courses",
                column: "TurnId",
                principalTable: "Turns",
                principalColumn: "Id");
        }
    }
}
