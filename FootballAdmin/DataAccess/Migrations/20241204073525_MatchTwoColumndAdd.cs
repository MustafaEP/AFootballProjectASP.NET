using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Migrations
{
    /// <inheritdoc />
    public partial class MatchTwoColumndAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isAccepted",
                table: "FootballMatches",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isRejected",
                table: "FootballMatches",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAccepted",
                table: "FootballMatches");

            migrationBuilder.DropColumn(
                name: "isRejected",
                table: "FootballMatches");
        }
    }
}
