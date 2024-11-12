using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Migrations
{
    /// <inheritdoc />
    public partial class newVersion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Footballers_ManagerClubs_ManagerClubId",
                table: "Footballers");

            migrationBuilder.DropIndex(
                name: "IX_Footballers_ManagerClubId",
                table: "Footballers");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Footballers");

            migrationBuilder.DropColumn(
                name: "Confidence",
                table: "Footballers");

            migrationBuilder.DropColumn(
                name: "DefendingUpgrade",
                table: "Footballers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Footballers");

            migrationBuilder.DropColumn(
                name: "DribblingUpgrade",
                table: "Footballers");

            migrationBuilder.DropColumn(
                name: "ManagerClubId",
                table: "Footballers");

            migrationBuilder.DropColumn(
                name: "PaceUpgrade",
                table: "Footballers");

            migrationBuilder.DropColumn(
                name: "PassingUpgrade",
                table: "Footballers");

            migrationBuilder.DropColumn(
                name: "PhysicalityUpgrade",
                table: "Footballers");

            migrationBuilder.DropColumn(
                name: "ShootingUpgrade",
                table: "Footballers");

            migrationBuilder.CreateTable(
                name: "ClubsFootballers",
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
                    PaceUpgrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShootingUpgrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassingUpgrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DribblingUpgrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefendingUpgrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhysicalityUpgrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Confidence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    ManagerClubId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubsFootballers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubsFootballers_ManagerClubs_ManagerClubId",
                        column: x => x.ManagerClubId,
                        principalTable: "ManagerClubs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubsFootballers_ManagerClubId",
                table: "ClubsFootballers",
                column: "ManagerClubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubsFootballers");

            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Footballers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Confidence",
                table: "Footballers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefendingUpgrade",
                table: "Footballers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Footballers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DribblingUpgrade",
                table: "Footballers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManagerClubId",
                table: "Footballers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaceUpgrade",
                table: "Footballers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassingUpgrade",
                table: "Footballers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhysicalityUpgrade",
                table: "Footballers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShootingUpgrade",
                table: "Footballers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Footballers_ManagerClubId",
                table: "Footballers",
                column: "ManagerClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Footballers_ManagerClubs_ManagerClubId",
                table: "Footballers",
                column: "ManagerClubId",
                principalTable: "ManagerClubs",
                principalColumn: "Id");
        }
    }
}
