using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Migrations
{
    /// <inheritdoc />
    public partial class formation_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Formations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagerClubId = table.Column<int>(type: "int", nullable: false),
                    FotmationType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Formations_ManagerClubs_ManagerClubId",
                        column: x => x.ManagerClubId,
                        principalTable: "ManagerClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "XIPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormationId = table.Column<int>(type: "int", nullable: false),
                    AddedFootballerId = table.Column<int>(type: "int", nullable: false),
                    position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XIPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XIPlayers_AddedFootballer_AddedFootballerId",
                        column: x => x.AddedFootballerId,
                        principalTable: "AddedFootballer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_XIPlayers_Formations_FormationId",
                        column: x => x.FormationId,
                        principalTable: "Formations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Formations_ManagerClubId",
                table: "Formations",
                column: "ManagerClubId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_XIPlayers_AddedFootballerId",
                table: "XIPlayers",
                column: "AddedFootballerId");

            migrationBuilder.CreateIndex(
                name: "IX_XIPlayers_FormationId",
                table: "XIPlayers",
                column: "FormationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "XIPlayers");

            migrationBuilder.DropTable(
                name: "Formations");
        }
    }
}
