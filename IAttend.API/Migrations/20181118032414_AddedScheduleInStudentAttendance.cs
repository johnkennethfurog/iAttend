using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IAttend.API.Migrations
{
    public partial class AddedScheduleInStudentAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            //     name: "Student_view",
            //     columns: table => new
            //     {
            //         StudentNumber = table.Column<string>(nullable: false),
            //         StudentName = table.Column<string>(nullable: true),
            //         Avatar = table.Column<string>(nullable: true),
            //         ContactPersonName = table.Column<string>(nullable: true),
            //         ContactPersonMobileNumber = table.Column<string>(nullable: true),
            //         ContactPersonRelations = table.Column<string>(nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Student_view", x => x.StudentNumber);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "Students_subjects_view",
            //     columns: table => new
            //     {
            //         StudentNumber = table.Column<string>(nullable: true),
            //         Instructor = table.Column<string>(nullable: true),
            //         InstructorNumber = table.Column<string>(nullable: true),
            //         Avatar = table.Column<string>(nullable: true),
            //         Room = table.Column<string>(nullable: true),
            //         Time = table.Column<DateTime>(nullable: false),
            //         DayOfWeek = table.Column<int>(nullable: false),
            //         SubjectCode = table.Column<string>(nullable: false),
            //         Subject = table.Column<string>(nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Students_subjects_view", x => x.SubjectCode);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "StudentsSubjectAttendances",
            //     columns: table => new
            //     {
            //         StudentNumber = table.Column<string>(nullable: false),
            //         StudentName = table.Column<string>(nullable: true),
            //         Avatar = table.Column<string>(nullable: true),
            //         IsScanned = table.Column<bool>(nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_StudentsSubjectAttendances", x => x.StudentNumber);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "TeacherSubjects",
            //     columns: table => new
            //     {
            //         ID = table.Column<int>(nullable: false),
            //         Room = table.Column<string>(nullable: true),
            //         Time = table.Column<DateTime>(nullable: false),
            //         DayOfWeek = table.Column<int>(nullable: false),
            //         SubjectCode = table.Column<string>(nullable: false),
            //         Subject = table.Column<string>(nullable: true),
            //         StudCount = table.Column<int>(nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_TeacherSubjects", x => x.SubjectCode);
            //     });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student_view");

            migrationBuilder.DropTable(
                name: "Students_subjects_view");

            migrationBuilder.DropTable(
                name: "StudentsSubjectAttendances");

            migrationBuilder.DropTable(
                name: "TeacherSubjects");
        }
    }
}
