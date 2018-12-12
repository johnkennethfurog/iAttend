using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IAttend.API.Migrations
{
    public partial class AddedTimeFromAndTimeTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Schedules",
                newName: "TimeTo");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeFrom",
                table: "Schedules",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeFrom",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "TimeTo",
                table: "Schedules",
                newName: "Time");
        }
    }
}
