using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Migrations
{
    /// <inheritdoc />
    public partial class createdUptatedTimeAddTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "TrainingSessions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UptatedTime",
                table: "TrainingSessions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Teams",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UptatedTime",
                table: "Teams",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Players",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UptatedTime",
                table: "Players",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Matches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UptatedTime",
                table: "Matches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Managers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UptatedTime",
                table: "Managers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Admins",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UptatedTime",
                table: "Admins",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "TrainingSessions");

            migrationBuilder.DropColumn(
                name: "UptatedTime",
                table: "TrainingSessions");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "UptatedTime",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "UptatedTime",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "UptatedTime",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "UptatedTime",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "UptatedTime",
                table: "Admins");
        }
    }
}
