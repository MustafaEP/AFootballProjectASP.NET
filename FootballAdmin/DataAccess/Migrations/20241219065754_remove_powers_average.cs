using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Migrations
{
    /// <inheritdoc />
    public partial class remove_powers_average : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Defending",
                table: "FootballerStatistics");

            migrationBuilder.DropColumn(
                name: "Dribbling",
                table: "FootballerStatistics");

            migrationBuilder.DropColumn(
                name: "Pace",
                table: "FootballerStatistics");

            migrationBuilder.DropColumn(
                name: "Passing",
                table: "FootballerStatistics");

            migrationBuilder.DropColumn(
                name: "Physicality",
                table: "FootballerStatistics");

            migrationBuilder.DropColumn(
                name: "Shooting",
                table: "FootballerStatistics");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Defending",
                table: "FootballerStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Dribbling",
                table: "FootballerStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Pace",
                table: "FootballerStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Passing",
                table: "FootballerStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Physicality",
                table: "FootballerStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Shooting",
                table: "FootballerStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
