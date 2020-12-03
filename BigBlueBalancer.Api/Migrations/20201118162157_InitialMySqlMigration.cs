using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BigBlueBalancer.Api.Migrations
{
    public partial class InitialMySqlMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Secret = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Up = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Load = table.Column<double>(type: "double", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ServerId = table.Column<short>(type: "smallint", nullable: false),
                    MeetingID = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    InternalMeetingID = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    ParentMeetingID = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    AttendeePW = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    ModeratorPW = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    CreateTime = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    VoiceBridge = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    DialNumber = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    CreateDate = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    HasUserJoined = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    HasBeenForciblyEnded = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Running = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meetings_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServerStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ServerId = table.Column<short>(type: "smallint", nullable: false),
                    Up = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MeetingsCount = table.Column<int>(type: "int", nullable: false),
                    ParticipantCount = table.Column<int>(type: "int", nullable: false),
                    ListenerCount = table.Column<int>(type: "int", nullable: false),
                    VoiceParticipantCount = table.Column<int>(type: "int", nullable: false),
                    VideoCount = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                name: "IX_Meetings_MeetingID",
                table: "Meetings",
                column: "MeetingID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_ServerId",
                table: "Meetings",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerStats_ServerId",
                table: "ServerStats",
                column: "ServerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "ServerStats");

            migrationBuilder.DropTable(
                name: "Servers");
        }
    }
}
