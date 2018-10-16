using Microsoft.EntityFrameworkCore.Migrations;

namespace IAttend.API.Migrations
{
    public partial class AdditionalRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Schedules_ScheduleID",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_Attendances_AttendanceID",
                table: "StudentAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Schedules_ScheduleID",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Students_StudentID",
                table: "StudentSubjects");

            migrationBuilder.AlterColumn<int>(
                name: "StudentID",
                table: "StudentSubjects",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleID",
                table: "StudentSubjects",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttendanceID",
                table: "StudentAttendances",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleID",
                table: "StudentAttendances",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleID",
                table: "Attendances",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendances_ScheduleID",
                table: "StudentAttendances",
                column: "ScheduleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Schedules_ScheduleID",
                table: "Attendances",
                column: "ScheduleID",
                principalTable: "Schedules",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_Attendances_AttendanceID",
                table: "StudentAttendances",
                column: "AttendanceID",
                principalTable: "Attendances",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_Schedules_ScheduleID",
                table: "StudentAttendances",
                column: "ScheduleID",
                principalTable: "Schedules",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Schedules_ScheduleID",
                table: "StudentSubjects",
                column: "ScheduleID",
                principalTable: "Schedules",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Students_StudentID",
                table: "StudentSubjects",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Schedules_ScheduleID",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_Attendances_AttendanceID",
                table: "StudentAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_Schedules_ScheduleID",
                table: "StudentAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Schedules_ScheduleID",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Students_StudentID",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentAttendances_ScheduleID",
                table: "StudentAttendances");

            migrationBuilder.DropColumn(
                name: "ScheduleID",
                table: "StudentAttendances");

            migrationBuilder.AlterColumn<int>(
                name: "StudentID",
                table: "StudentSubjects",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleID",
                table: "StudentSubjects",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AttendanceID",
                table: "StudentAttendances",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleID",
                table: "Attendances",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Schedules_ScheduleID",
                table: "Attendances",
                column: "ScheduleID",
                principalTable: "Schedules",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_Attendances_AttendanceID",
                table: "StudentAttendances",
                column: "AttendanceID",
                principalTable: "Attendances",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Schedules_ScheduleID",
                table: "StudentSubjects",
                column: "ScheduleID",
                principalTable: "Schedules",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Students_StudentID",
                table: "StudentSubjects",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
