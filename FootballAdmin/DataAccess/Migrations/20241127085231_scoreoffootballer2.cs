using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Migrations
{
    /// <inheritdoc />
    public partial class scoreoffootballer2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "GoalExpectetion",
                table: "ScoreofFootballers",
                type: "real",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "GoalExpectetion",
                table: "ScoreofFootballers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
