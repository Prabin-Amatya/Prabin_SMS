using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prabin_SMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedDegCourseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "DegreeCourse",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Semester",
                table: "DegreeCourse");
        }
    }
}
