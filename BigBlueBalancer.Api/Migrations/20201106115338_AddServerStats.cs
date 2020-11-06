using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BigBlueBalancer.Api.Migrations
{
    public partial class AddServerStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastChecked",
                table: "Servers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ListenerCount",
                table: "Servers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParticipantCount",
                table: "Servers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Up",
                table: "Servers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "VideoCount",
                table: "Servers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VoiceParticipantCount",
                table: "Servers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastChecked",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "ListenerCount",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "ParticipantCount",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "Up",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "VideoCount",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "VoiceParticipantCount",
                table: "Servers");
        }
    }
}
