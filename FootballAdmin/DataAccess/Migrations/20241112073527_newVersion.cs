using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Migrations
{
    /// <inheritdoc />
    public partial class newVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerClubId",
                table: "Managers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManagerClubId1",
                table: "Managers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ManagerClubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    LineUp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UptatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerClubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Footballers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltPositions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferredFoot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pace = table.Column<int>(type: "int", nullable: false),
                    Shooting = table.Column<int>(type: "int", nullable: false),
                    Passing = table.Column<int>(type: "int", nullable: false),
                    Dribbling = table.Column<int>(type: "int", nullable: false),
                    Defending = table.Column<int>(type: "int", nullable: false),
                    Physicality = table.Column<int>(type: "int", nullable: false),
                    Abilities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    PaceUpgrade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShootingUpgrade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassingUpgrade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DribblingUpgrade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefendingUpgrade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalityUpgrade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Confidence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClubId = table.Column<int>(type: "int", nullable: true),
                    ManagerClubId = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UptatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Footballers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Footballers_ManagerClubs_ManagerClubId",
                        column: x => x.ManagerClubId,
                        principalTable: "ManagerClubs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Managers_ManagerClubId1",
                table: "Managers",
                column: "ManagerClubId1");

            migrationBuilder.CreateIndex(
                name: "IX_Footballers_ManagerClubId",
                table: "Footballers",
                column: "ManagerClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_ManagerClubs_ManagerClubId1",
                table: "Managers",
                column: "ManagerClubId1",
                principalTable: "ManagerClubs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_ManagerClubs_ManagerClubId1",
                table: "Managers");

            migrationBuilder.DropTable(
                name: "Footballers");

            migrationBuilder.DropTable(
                name: "ManagerClubs");

            migrationBuilder.DropIndex(
                name: "IX_Managers_ManagerClubId1",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "ManagerClubId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "ManagerClubId1",
                table: "Managers");
        }
    }
}
