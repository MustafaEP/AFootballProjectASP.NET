using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Migrations
{
    /// <inheritdoc />
    public partial class addTeamIdinManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Managers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Managers");
        }
    }
}
