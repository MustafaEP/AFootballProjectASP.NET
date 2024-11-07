using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Migrations
{
    /// <inheritdoc />
    public partial class add_columnPlayers_TrainingSessionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Players",
                table: "TrainingSessions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Players",
                table: "TrainingSessions");
        }
    }
}
