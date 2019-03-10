using Microsoft.EntityFrameworkCore.Migrations;

namespace IAttend.API.Migrations
{
    public partial class addedCascadeDeleteAtScheduleModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_Schedules_ScheduleID",
                table: "StudentAttendances");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_Schedules_ScheduleID",
                table: "StudentAttendances",
                column: "ScheduleID",
                principalTable: "Schedules",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_Schedules_ScheduleID",
                table: "StudentAttendances");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_Schedules_ScheduleID",
                table: "StudentAttendances",
                column: "ScheduleID",
                principalTable: "Schedules",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
