using Microsoft.EntityFrameworkCore.Migrations;

namespace IAttend.API.Migrations
{
    public partial class AddedAdditionalCasecadeDeleteAtSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Schedules_ScheduleID",
                table: "StudentSubjects");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Schedules_ScheduleID",
                table: "StudentSubjects",
                column: "ScheduleID",
                principalTable: "Schedules",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Schedules_ScheduleID",
                table: "StudentSubjects");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Schedules_ScheduleID",
                table: "StudentSubjects",
                column: "ScheduleID",
                principalTable: "Schedules",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
