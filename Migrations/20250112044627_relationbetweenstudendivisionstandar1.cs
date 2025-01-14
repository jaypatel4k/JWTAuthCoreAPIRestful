using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTAuthCoreAPIRestful.Migrations
{
    /// <inheritdoc />
    public partial class relationbetweenstudendivisionstandar1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DivisionId",
                table: "Student",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "DivisionId",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
