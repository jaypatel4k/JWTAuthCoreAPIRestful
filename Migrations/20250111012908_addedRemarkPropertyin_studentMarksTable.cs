using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTAuthCoreAPIRestful.Migrations
{
    /// <inheritdoc />
    public partial class addedRemarkPropertyin_studentMarksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "StudentMark",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "StudentMark");
        }
    }
}
