using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BigBlueBalancer.Api.Migrations
{
    public partial class AddServerStatsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ServerStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServerId = table.Column<short>(type: "smallint", nullable: false),
                    Up = table.Column<bool>(type: "bit", nullable: false),
                    MeetingsCount = table.Column<int>(type: "int", nullable: false),
                    ParticipantCount = table.Column<int>(type: "int", nullable: false),
                    ListenerCount = table.Column<int>(type: "int", nullable: false),
                    VoiceParticipantCount = table.Column<int>(type: "int", nullable: false),
                    VideoCount = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerStats_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServerStats_ServerId",
                table: "ServerStats",
                column: "ServerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServerStats");

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
    }
}
