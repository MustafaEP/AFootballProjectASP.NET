using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Migrations
{
    /// <inheritdoc />
    public partial class footballMatchTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FootballMatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    HomeClubId = table.Column<int>(type: "int", nullable: false),
                    AwayClubId = table.Column<int>(type: "int", nullable: false),
                    MatchString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Goals = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Asists = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeRedCards = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwayRedCards = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeYellowCards = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwayYellowCards = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeCorners = table.Column<int>(type: "int", nullable: false),
                    AwayCorners = table.Column<int>(type: "int", nullable: false),
                    HomeSetPlay = table.Column<int>(type: "int", nullable: false),
                    AwaySetPlay = table.Column<int>(type: "int", nullable: false),
                    HomeBallPlaying = table.Column<int>(type: "int", nullable: false),
                    AwayBallPlaying = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UptatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FootballMatches_ManagerClubs_AwayClubId",
                        column: x => x.AwayClubId,
                        principalTable: "ManagerClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FootballMatches_ManagerClubs_HomeClubId",
                        column: x => x.HomeClubId,
                        principalTable: "ManagerClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FootballMatches_AwayClubId",
                table: "FootballMatches",
                column: "AwayClubId");

            migrationBuilder.CreateIndex(
                name: "IX_FootballMatches_HomeClubId",
                table: "FootballMatches",
                column: "HomeClubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FootballMatches");
        }
    }
}
