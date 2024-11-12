using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Migrations
{
    /// <inheritdoc />
    public partial class newVersion3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_ManagerClubs_ManagerClubId1",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_ManagerClubId1",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "ManagerClubId1",
                table: "Managers");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_ManagerClubId",
                table: "Managers",
                column: "ManagerClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_ManagerClubs_ManagerClubId",
                table: "Managers",
                column: "ManagerClubId",
                principalTable: "ManagerClubs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_ManagerClubs_ManagerClubId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_ManagerClubId",
                table: "Managers");

            migrationBuilder.AddColumn<int>(
                name: "ManagerClubId1",
                table: "Managers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Managers_ManagerClubId1",
                table: "Managers",
                column: "ManagerClubId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_ManagerClubs_ManagerClubId1",
                table: "Managers",
                column: "ManagerClubId1",
                principalTable: "ManagerClubs",
                principalColumn: "Id");
        }
    }
}
