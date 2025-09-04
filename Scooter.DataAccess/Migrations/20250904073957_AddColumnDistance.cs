using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnDistance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "costnok",
                table: "trip",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<double>(
                name: "distancekm",
                table: "trip",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "distancekm",
                table: "trip");

            migrationBuilder.AlterColumn<int>(
                name: "costnok",
                table: "trip",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
