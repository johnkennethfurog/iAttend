﻿// <auto-generated />
using System;
using IAttend.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IAttend.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20181212135913_AddedDropFieldAtStudent")]
    partial class AddedDropFieldAtStudent
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IAttend.API.Models.Attendance", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsOpen");

                    b.Property<int>("ScheduleID");

                    b.Property<DateTime>("TimeStarted");

                    b.HasKey("ID");

                    b.HasIndex("ScheduleID");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("IAttend.API.Models.ContactPerson", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MobileNumber");

                    b.Property<string>("Name");

                    b.Property<string>("RelationToStudent");

                    b.HasKey("ID");

                    b.ToTable("ContactPersons");
                });

            modelBuilder.Entity("IAttend.API.Models.Instructor", b =>
                {
                    b.Property<string>("InstructorNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.HasKey("InstructorNumber");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("IAttend.API.Models.Schedule", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DayOfWeek");

                    b.Property<string>("InstructorNumber");

                    b.Property<string>("Room");

                    b.Property<string>("SubjectCode");

                    b.Property<DateTime>("TimeFrom");

                    b.Property<DateTime>("TimeTo");

                    b.HasKey("ID");

                    b.HasIndex("InstructorNumber");

                    b.HasIndex("SubjectCode");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("IAttend.API.Models.Student", b =>
                {
                    b.Property<string>("StudentNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<int?>("ContactPersonID");

                    b.Property<bool>("IsDropped");

                    b.Property<string>("StudentName");

                    b.HasKey("StudentNumber");

                    b.HasIndex("ContactPersonID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("IAttend.API.Models.StudentAttendance", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AttendanceID");

                    b.Property<bool>("IsScanned");

                    b.Property<int?>("ScheduleID");

                    b.Property<string>("StudentNumber");

                    b.Property<DateTime>("Time");

                    b.HasKey("ID");

                    b.HasIndex("AttendanceID");

                    b.HasIndex("ScheduleID");

                    b.ToTable("StudentAttendances");
                });

            modelBuilder.Entity("IAttend.API.Models.StudentSubject", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ScheduleID");

                    b.Property<string>("StudentNumber");

                    b.HasKey("ID");

                    b.HasIndex("ScheduleID");

                    b.HasIndex("StudentNumber");

                    b.ToTable("StudentSubjects");
                });

            modelBuilder.Entity("IAttend.API.Models.Subject", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Code");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("IAttend.API.Pocos.Schedule", b =>
                {
                    b.Property<int>("ScheduleID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Avatar");

                    b.Property<int>("DayOfWeek");

                    b.Property<string>("Instructor");

                    b.Property<string>("Room");

                    b.Property<string>("Subject");

                    b.Property<string>("SubjectCode");

                    b.Property<DateTime>("TimeFrom");

                    b.Property<DateTime>("TimeTo");

                    b.HasKey("ScheduleID");

                    b.ToTable("Room_schedule_view");
                });

            modelBuilder.Entity("IAttend.API.Pocos.Student", b =>
                {
                    b.Property<string>("StudentNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<string>("ContactPersonMobileNumber");

                    b.Property<string>("ContactPersonName");

                    b.Property<string>("ContactPersonRelations");

                    b.Property<string>("StudentName");

                    b.HasKey("StudentNumber");

                    b.ToTable("Student_view");
                });

            modelBuilder.Entity("IAttend.API.Pocos.StudentsAbsentStat", b =>
                {
                    b.Property<string>("StudentNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Absent");

                    b.Property<string>("Avatar");

                    b.Property<string>("InstructorNumber");

                    b.Property<int>("Present");

                    b.Property<string>("Room");

                    b.Property<string>("StudentName");

                    b.Property<string>("Subject");

                    b.Property<string>("SubjectCode");

                    b.Property<string>("Time");

                    b.Property<int>("TotalAttendance");

                    b.HasKey("StudentNumber");

                    b.ToTable("StudentsAttendanceStats");
                });

            modelBuilder.Entity("IAttend.API.Pocos.StudentsSubjectAttendance", b =>
                {
                    b.Property<string>("StudentNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<bool?>("IsScanned");

                    b.Property<string>("StudentName");

                    b.HasKey("StudentNumber");

                    b.ToTable("StudentsSubjectAttendances");
                });

            modelBuilder.Entity("IAttend.API.Pocos.StudentSubject", b =>
                {
                    b.Property<int>("ScheduleID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Avatar");

                    b.Property<int>("DayOfWeek");

                    b.Property<string>("Instructor");

                    b.Property<string>("InstructorNumber");

                    b.Property<string>("Room");

                    b.Property<string>("StudentNumber");

                    b.Property<string>("Subject");

                    b.Property<string>("SubjectCode");

                    b.Property<DateTime>("TimeFrom");

                    b.Property<DateTime>("TimeTo");

                    b.HasKey("ScheduleID");

                    b.ToTable("Students_subjects_view");
                });

            modelBuilder.Entity("IAttend.API.Pocos.TeacherSubject", b =>
                {
                    b.Property<string>("SubjectCode")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DayOfWeek");

                    b.Property<int>("ID");

                    b.Property<int>("IsOpen");

                    b.Property<string>("Room");

                    b.Property<int>("StudCount");

                    b.Property<string>("Subject");

                    b.Property<DateTime>("TimeFrom");

                    b.Property<DateTime>("TimeTo");

                    b.HasKey("SubjectCode");

                    b.ToTable("TeacherSubjects");
                });

            modelBuilder.Entity("IAttend.API.Models.Attendance", b =>
                {
                    b.HasOne("IAttend.API.Models.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IAttend.API.Models.Schedule", b =>
                {
                    b.HasOne("IAttend.API.Models.Instructor", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorNumber");

                    b.HasOne("IAttend.API.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectCode");
                });

            modelBuilder.Entity("IAttend.API.Models.Student", b =>
                {
                    b.HasOne("IAttend.API.Models.ContactPerson", "ContactPerson")
                        .WithMany()
                        .HasForeignKey("ContactPersonID");
                });

            modelBuilder.Entity("IAttend.API.Models.StudentAttendance", b =>
                {
                    b.HasOne("IAttend.API.Models.Attendance", "Attendance")
                        .WithMany("StudentAttendances")
                        .HasForeignKey("AttendanceID");

                    b.HasOne("IAttend.API.Models.Schedule", "Schedule")
                        .WithMany("StudentAttendances")
                        .HasForeignKey("ScheduleID");
                });

            modelBuilder.Entity("IAttend.API.Models.StudentSubject", b =>
                {
                    b.HasOne("IAttend.API.Models.Schedule", "Schedule")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("ScheduleID");

                    b.HasOne("IAttend.API.Models.Student", "Student")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("StudentNumber");
                });
#pragma warning restore 612, 618
        }
    }
}
