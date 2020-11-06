using Microsoft.EntityFrameworkCore.Migrations;

namespace BigBlueBalancer.Api.Migrations
{
    public partial class AddAggregatesToServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Load",
                table: "Servers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "Up",
                table: "Servers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Load",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "Up",
                table: "Servers");
        }
    }
}
