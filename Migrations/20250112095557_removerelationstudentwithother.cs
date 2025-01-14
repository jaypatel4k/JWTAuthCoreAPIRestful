using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTAuthCoreAPIRestful.Migrations
{
    /// <inheritdoc />
    public partial class removerelationstudentwithother : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Division_DivisionId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Standard_StandId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_DivisionId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_StandId",
                table: "Student");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Student_DivisionId",
                table: "Student",
                column: "DivisionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_StandId",
                table: "Student",
                column: "StandId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Division_DivisionId",
                table: "Student",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Standard_StandId",
                table: "Student",
                column: "StandId",
                principalTable: "Standard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
