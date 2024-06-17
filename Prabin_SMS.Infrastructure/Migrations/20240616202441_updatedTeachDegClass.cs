using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prabin_SMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedTeachDegClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TeacherDegree");

            migrationBuilder.DropColumn(
                name: "Section",
                table: "TeacherDegree");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "TeacherDegree");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "TeacherDegree");

            migrationBuilder.CreateTable(
                name: "TeacherClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Semester = table.Column<int>(type: "int", nullable: true),
                    Section = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherClassTeacherDegree",
                columns: table => new
                {
                    TeacherClassesId = table.Column<int>(type: "int", nullable: false),
                    TeacherDegreesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherClassTeacherDegree", x => new { x.TeacherClassesId, x.TeacherDegreesId });
                    table.ForeignKey(
                        name: "FK_TeacherClassTeacherDegree_TeacherClass_TeacherClassesId",
                        column: x => x.TeacherClassesId,
                        principalTable: "TeacherClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherClassTeacherDegree_TeacherDegree_TeacherDegreesId",
                        column: x => x.TeacherDegreesId,
                        principalTable: "TeacherDegree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherDegreeClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherClassId = table.Column<int>(type: "int", nullable: false),
                    TeacherDegreeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherDegreeClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherDegreeClass_TeacherClass_TeacherClassId",
                        column: x => x.TeacherClassId,
                        principalTable: "TeacherClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherDegreeClass_TeacherDegree_TeacherDegreeId",
                        column: x => x.TeacherDegreeId,
                        principalTable: "TeacherDegree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherClassTeacherDegree_TeacherDegreesId",
                table: "TeacherClassTeacherDegree",
                column: "TeacherDegreesId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherDegreeClass_TeacherClassId",
                table: "TeacherDegreeClass",
                column: "TeacherClassId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherDegreeClass_TeacherDegreeId",
                table: "TeacherDegreeClass",
                column: "TeacherDegreeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherClassTeacherDegree");

            migrationBuilder.DropTable(
                name: "TeacherDegreeClass");

            migrationBuilder.DropTable(
                name: "TeacherClass");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "TeacherDegree",
                type: "TIME",
                nullable: true);

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

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "TeacherDegree",
                type: "TIME",
                nullable: true);
        }
    }
}
