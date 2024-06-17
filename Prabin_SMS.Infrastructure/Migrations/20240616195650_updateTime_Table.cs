using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prabin_SMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateTime_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "TeacherDegree",
                type: "TIME",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "TeacherDegree",
                type: "TIME",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TeacherDegree");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "TeacherDegree");
        }
    }
}
