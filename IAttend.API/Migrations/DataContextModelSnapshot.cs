﻿// <auto-generated />
using System;
using IAttend.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IAttend.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<DateTime>("Time");

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

                    b.HasOne("IAttend.API.Models.Schedule")
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
