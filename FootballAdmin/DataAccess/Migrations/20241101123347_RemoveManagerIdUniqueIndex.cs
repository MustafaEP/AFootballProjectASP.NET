using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Migrations
{
    /// <inheritdoc />
    public partial class RemoveManagerIdUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_ManagerId",
                table: "Teams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Teams_ManagerId",
                table: "Teams",
                column: "ManagerId",
                unique: true);
        }
    }
}
