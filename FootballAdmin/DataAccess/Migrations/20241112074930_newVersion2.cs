using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Migrations
{
    /// <inheritdoc />
    public partial class newVersion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubsFootballers_ManagerClubs_ManagerClubId",
                table: "ClubsFootballers");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "ClubsFootballers");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerClubId",
                table: "ClubsFootballers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "ClubsFootballers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UptatedTime",
                table: "ClubsFootballers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubsFootballers_ManagerClubs_ManagerClubId",
                table: "ClubsFootballers",
                column: "ManagerClubId",
                principalTable: "ManagerClubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubsFootballers_ManagerClubs_ManagerClubId",
                table: "ClubsFootballers");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "ClubsFootballers");

            migrationBuilder.DropColumn(
                name: "UptatedTime",
                table: "ClubsFootballers");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerClubId",
                table: "ClubsFootballers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "ClubsFootballers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubsFootballers_ManagerClubs_ManagerClubId",
                table: "ClubsFootballers",
                column: "ManagerClubId",
                principalTable: "ManagerClubs",
                principalColumn: "Id");
        }
    }
}
