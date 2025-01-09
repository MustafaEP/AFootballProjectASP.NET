using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Migrations
{
    /// <inheritdoc />
    public partial class changedEveryThing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddedFootballer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FootballerId = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddedFootballer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddedFootballer_Footballers_FootballerId",
                        column: x => x.FootballerId,
                        principalTable: "Footballers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddedFootballer_ManagerClubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "ManagerClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddedFootballerUpgrade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddedFootballerId = table.Column<int>(type: "int", nullable: false),
                    UpgradeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaceChange = table.Column<int>(type: "int", nullable: false),
                    ShootingChange = table.Column<int>(type: "int", nullable: false),
                    PassingChange = table.Column<int>(type: "int", nullable: false),
                    DribblingChange = table.Column<int>(type: "int", nullable: false),
                    DefendingChange = table.Column<int>(type: "int", nullable: false),
                    PhysicalityChange = table.Column<int>(type: "int", nullable: false),
                    Confidence = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddedFootballerUpgrade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddedFootballerUpgrade_AddedFootballer_AddedFootballerId",
                        column: x => x.AddedFootballerId,
                        principalTable: "AddedFootballer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddedFootballer_ClubId",
                table: "AddedFootballer",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_AddedFootballer_FootballerId",
                table: "AddedFootballer",
                column: "FootballerId");

            migrationBuilder.CreateIndex(
                name: "IX_AddedFootballerUpgrade_AddedFootballerId",
                table: "AddedFootballerUpgrade",
                column: "AddedFootballerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddedFootballerUpgrade");

            migrationBuilder.DropTable(
                name: "AddedFootballer");
        }
    }
}
