using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BigBlueBalancer.Api.Migrations
{
    public partial class AddMeetingEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServerId = table.Column<short>(type: "smallint", nullable: false),
                    MeetingID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InternalMeetingID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentMeetingID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttendeePW = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModeratorPW = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VoiceBridge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasUserJoined = table.Column<bool>(type: "bit", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    HasBeenForciblyEnded = table.Column<bool>(type: "bit", nullable: false),
                    Running = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_MeetingID",
                table: "Meetings",
                column: "MeetingID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_ServerId",
                table: "Meetings",
                column: "ServerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meetings");
        }
    }
}
