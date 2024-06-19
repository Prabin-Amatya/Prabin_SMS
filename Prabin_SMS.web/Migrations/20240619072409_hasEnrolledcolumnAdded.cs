using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prabin_SMS.web.Migrations
{
    /// <inheritdoc />
    public partial class hasEnrolledcolumnAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasEnrolled",
                table: "AspNetUsers",
                type: "bit",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasEnrolled",
                table: "AspNetUsers");
        }
    }
}
