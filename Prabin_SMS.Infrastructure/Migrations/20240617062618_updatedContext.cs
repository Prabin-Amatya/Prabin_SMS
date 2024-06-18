using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prabin_SMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RemainingSeats",
                table: "Degree",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalSeats",
                table: "Degree",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingSeats",
                table: "Degree");

            migrationBuilder.DropColumn(
                name: "TotalSeats",
                table: "Degree");
        }
    }
}
