using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prabin_SMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_table12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Section",
                table: "TeacherDegree",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "TeacherDegree",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Degree",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Section",
                table: "TeacherDegree");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "TeacherDegree");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Degree");
        }
    }
}
