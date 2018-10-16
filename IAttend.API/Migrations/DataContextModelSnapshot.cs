﻿// <auto-generated />
using System;
using IAttend.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("IAttend.API.Models.Attendance", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

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
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MobileNumber");

                    b.Property<string>("Name");

                    b.Property<string>("RelationToStudent");

                    b.HasKey("ID");

                    b.ToTable("ContactPersons");
                });

            modelBuilder.Entity("IAttend.API.Models.Instructor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("IAttend.API.Models.Schedule", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DayOfWeek");

                    b.Property<int?>("InstructorID");

                    b.Property<string>("Room");

                    b.Property<int?>("SubjectID");

                    b.Property<DateTime>("Time");

                    b.HasKey("ID");

                    b.HasIndex("InstructorID");

                    b.HasIndex("SubjectID");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("IAttend.API.Models.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<int?>("ContactPersonID");

                    b.Property<string>("StudentName");

                    b.Property<string>("StudentNumber");

                    b.HasKey("ID");

                    b.HasIndex("ContactPersonID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("IAttend.API.Models.StudentAttendance", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AttendanceID");

                    b.Property<bool>("IsScanned");

                    b.Property<int?>("ScheduleID");

                    b.Property<int?>("StudentID");

                    b.Property<DateTime>("Time");

                    b.HasKey("ID");

                    b.HasIndex("AttendanceID");

                    b.HasIndex("ScheduleID");

                    b.HasIndex("StudentID");

                    b.ToTable("StudentAttendances");
                });

            modelBuilder.Entity("IAttend.API.Models.StudentSubject", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ScheduleID");

                    b.Property<int>("StudentID");

                    b.HasKey("ID");

                    b.HasIndex("ScheduleID");

                    b.HasIndex("StudentID");

                    b.ToTable("StudentSubjects");
                });

            modelBuilder.Entity("IAttend.API.Models.Subject", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Name");

                    b.HasKey("ID");

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
                        .HasForeignKey("InstructorID");

                    b.HasOne("IAttend.API.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectID");
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
                        .HasForeignKey("AttendanceID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IAttend.API.Models.Schedule")
                        .WithMany("StudentAttendances")
                        .HasForeignKey("ScheduleID");

                    b.HasOne("IAttend.API.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentID");
                });

            modelBuilder.Entity("IAttend.API.Models.StudentSubject", b =>
                {
                    b.HasOne("IAttend.API.Models.Schedule", "Schedule")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("ScheduleID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IAttend.API.Models.Student", "Student")
                        .WithMany("Subjects")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
