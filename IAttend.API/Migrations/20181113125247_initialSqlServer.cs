using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IAttend.API.Migrations
{
    public partial class initialSqlServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactPersons",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    RelationToStudent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPersons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    InstructorNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentNumber = table.Column<string>(nullable: true),
                    StudentName = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    ContactPersonID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Students_ContactPersons_ContactPersonID",
                        column: x => x.ContactPersonID,
                        principalTable: "ContactPersons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SubjectID = table.Column<int>(nullable: true),
                    InstructorID = table.Column<int>(nullable: true),
                    Room = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    DayOfWeek = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Schedules_Instructors_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "Instructors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ScheduleID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    TimeStarted = table.Column<DateTime>(nullable: false),
                    IsOpen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Attendances_Schedules_ScheduleID",
                        column: x => x.ScheduleID,
                        principalTable: "Schedules",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSubjects",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentID = table.Column<int>(nullable: true),
                    ScheduleID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubjects", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StudentSubjects_Schedules_ScheduleID",
                        column: x => x.ScheduleID,
                        principalTable: "Schedules",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentSubjects_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentAttendances",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentNumber = table.Column<string>(nullable: true),
                    AttendanceID = table.Column<int>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    IsScanned = table.Column<bool>(nullable: false),
                    ScheduleID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAttendances", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StudentAttendances_Attendances_AttendanceID",
                        column: x => x.AttendanceID,
                        principalTable: "Attendances",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAttendances_Schedules_ScheduleID",
                        column: x => x.ScheduleID,
                        principalTable: "Schedules",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ScheduleID",
                table: "Attendances",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_InstructorID",
                table: "Schedules",
                column: "InstructorID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SubjectID",
                table: "Schedules",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendances_AttendanceID",
                table: "StudentAttendances",
                column: "AttendanceID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendances_ScheduleID",
                table: "StudentAttendances",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ContactPersonID",
                table: "Students",
                column: "ContactPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_ScheduleID",
                table: "StudentSubjects",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_StudentID",
                table: "StudentSubjects",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAttendances");

            migrationBuilder.DropTable(
                name: "StudentSubjects");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "ContactPersons");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
