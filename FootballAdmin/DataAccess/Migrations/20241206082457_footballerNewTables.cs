using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Migrations
{
    /// <inheritdoc />
    public partial class footballerNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FootballerPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FootballerId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballerPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FootballerPositions_Footballers_FootballerId",
                        column: x => x.FootballerId,
                        principalTable: "Footballers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FootballerStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FootballerId = table.Column<int>(type: "int", nullable: false),
                    Pace = table.Column<int>(type: "int", nullable: false),
                    Shooting = table.Column<int>(type: "int", nullable: false),
                    Passing = table.Column<int>(type: "int", nullable: false),
                    Dribbling = table.Column<int>(type: "int", nullable: false),
                    Defending = table.Column<int>(type: "int", nullable: false),
                    Physicality = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballerStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FootballerStatistics_Footballers_FootballerId",
                        column: x => x.FootballerId,
                        principalTable: "Footballers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FootballerAbilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FootballerStatisticId = table.Column<int>(type: "int", nullable: false),
                    AbilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballerAbilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FootballerAbilities_FootballerStatistics_FootballerStatisticId",
                        column: x => x.FootballerStatisticId,
                        principalTable: "FootballerStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FootballerDefendings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Interceptions = table.Column<int>(type: "int", nullable: false),
                    HeadingAccuracy = table.Column<int>(type: "int", nullable: false),
                    DefAwareness = table.Column<int>(type: "int", nullable: false),
                    StandingTackle = table.Column<int>(type: "int", nullable: false),
                    SlidingTackle = table.Column<int>(type: "int", nullable: false),
                    StatisticsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballerDefendings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FootballerDefendings_FootballerStatistics_StatisticsId",
                        column: x => x.StatisticsId,
                        principalTable: "FootballerStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FootballerDribblings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Agility = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    Reactions = table.Column<int>(type: "int", nullable: false),
                    BallControl = table.Column<int>(type: "int", nullable: false),
                    DribblingP = table.Column<int>(type: "int", nullable: false),
                    Composure = table.Column<int>(type: "int", nullable: false),
                    StatisticsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballerDribblings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FootballerDribblings_FootballerStatistics_StatisticsId",
                        column: x => x.StatisticsId,
                        principalTable: "FootballerStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FootballerPaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Acceleration = table.Column<int>(type: "int", nullable: false),
                    SprintSpeed = table.Column<int>(type: "int", nullable: false),
                    StatisticsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballerPaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FootballerPaces_FootballerStatistics_StatisticsId",
                        column: x => x.StatisticsId,
                        principalTable: "FootballerStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FootballerPassings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vision = table.Column<int>(type: "int", nullable: false),
                    Crossing = table.Column<int>(type: "int", nullable: false),
                    FreeKickAccuracy = table.Column<int>(type: "int", nullable: false),
                    ShortPassing = table.Column<int>(type: "int", nullable: false),
                    LongPassing = table.Column<int>(type: "int", nullable: false),
                    Curve = table.Column<int>(type: "int", nullable: false),
                    StatisticsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballerPassings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FootballerPassings_FootballerStatistics_StatisticsId",
                        column: x => x.StatisticsId,
                        principalTable: "FootballerStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "footballerPhysicalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jumping = table.Column<int>(type: "int", nullable: false),
                    Stamina = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    Aggression = table.Column<int>(type: "int", nullable: false),
                    StatisticsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_footballerPhysicalities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_footballerPhysicalities_FootballerStatistics_StatisticsId",
                        column: x => x.StatisticsId,
                        principalTable: "FootballerStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FootballerShootings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Positioning = table.Column<int>(type: "int", nullable: false),
                    Finishing = table.Column<int>(type: "int", nullable: false),
                    ShotPower = table.Column<int>(type: "int", nullable: false),
                    LongShots = table.Column<int>(type: "int", nullable: false),
                    Volleys = table.Column<int>(type: "int", nullable: false),
                    Penalties = table.Column<int>(type: "int", nullable: false),
                    StatisticsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballerShootings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FootballerShootings_FootballerStatistics_StatisticsId",
                        column: x => x.StatisticsId,
                        principalTable: "FootballerStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FootballerAbilities_FootballerStatisticId",
                table: "FootballerAbilities",
                column: "FootballerStatisticId");

            migrationBuilder.CreateIndex(
                name: "IX_FootballerDefendings_StatisticsId",
                table: "FootballerDefendings",
                column: "StatisticsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FootballerDribblings_StatisticsId",
                table: "FootballerDribblings",
                column: "StatisticsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FootballerPaces_StatisticsId",
                table: "FootballerPaces",
                column: "StatisticsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FootballerPassings_StatisticsId",
                table: "FootballerPassings",
                column: "StatisticsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_footballerPhysicalities_StatisticsId",
                table: "footballerPhysicalities",
                column: "StatisticsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FootballerPositions_FootballerId",
                table: "FootballerPositions",
                column: "FootballerId");

            migrationBuilder.CreateIndex(
                name: "IX_FootballerShootings_StatisticsId",
                table: "FootballerShootings",
                column: "StatisticsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FootballerStatistics_FootballerId",
                table: "FootballerStatistics",
                column: "FootballerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FootballerAbilities");

            migrationBuilder.DropTable(
                name: "FootballerDefendings");

            migrationBuilder.DropTable(
                name: "FootballerDribblings");

            migrationBuilder.DropTable(
                name: "FootballerPaces");

            migrationBuilder.DropTable(
                name: "FootballerPassings");

            migrationBuilder.DropTable(
                name: "footballerPhysicalities");

            migrationBuilder.DropTable(
                name: "FootballerPositions");

            migrationBuilder.DropTable(
                name: "FootballerShootings");

            migrationBuilder.DropTable(
                name: "FootballerStatistics");
        }
    }
}
