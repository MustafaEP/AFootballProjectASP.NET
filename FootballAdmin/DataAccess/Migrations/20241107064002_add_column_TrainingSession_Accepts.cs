using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Migrations
{
    /// <inheritdoc />
    public partial class add_column_TrainingSession_Accepts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Accepts",
                table: "TrainingSessions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepts",
                table: "TrainingSessions");
        }
    }
}
