using IAttend.API.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IAttend.API.Data
{

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAttendance> StudentAttendances { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        #region Table Value Functions

        public DbSet<Pocos.StudentsSubjectAttendance> StudentsSubjectAttendances { get; set; }
        public DbSet<Pocos.TeacherSubject> TeacherSubjects { get; set; }

        #endregion

        #region  Views
        public DbSet<Pocos.Schedule> RoomSchedulesView { get; set; }
        public DbSet<Pocos.Student> StudentsView{ get; set; }
        public DbSet<Pocos.StudentSubject> StudentsSubjectsView{ get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentViewConfiguration());
            modelBuilder.ApplyConfiguration(new StudentsSubjectsViewConfiguration());
            modelBuilder.ApplyConfiguration(new RoomScheduleViewConfiguration());

            modelBuilder.ApplyConfiguration(new StudentsSubjectAttendanceConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherSubjectConfiguration());

        }

    }

    #region  ViewConfiguration

    public class StudentViewConfiguration : IEntityTypeConfiguration<Pocos.Student>
    {
        public void Configure(EntityTypeBuilder<Pocos.Student> builder)
        {
            builder.HasKey(x => x.StudentNumber);
            builder.ToTable("Student_view");
        }
    }

    public class RoomScheduleViewConfiguration : IEntityTypeConfiguration<Pocos.Schedule>
    {
        public void Configure(EntityTypeBuilder<Pocos.Schedule> builder)
        {
            builder.HasKey(x => x.ScheduleID);
            builder.ToTable("Room_schedule_view");
        }
    }

    public class StudentsSubjectsViewConfiguration : IEntityTypeConfiguration<Pocos.StudentSubject>
    {
        public void Configure(EntityTypeBuilder<Pocos.StudentSubject> builder)
        {
            builder.HasKey(x => x.ScheduleID);
            builder.ToTable("Students_subjects_view");
        }
    }

    #endregion

    #region TVFConfiguration

    public class StudentsSubjectAttendanceConfiguration : IEntityTypeConfiguration<Pocos.StudentsSubjectAttendance>
    {
        public void Configure(EntityTypeBuilder<Pocos.StudentsSubjectAttendance> builder)
        {
            builder.HasKey(x => x.StudentNumber);
        }
    }

    public class TeacherSubjectConfiguration : IEntityTypeConfiguration<Pocos.TeacherSubject>
    {
        public void Configure(EntityTypeBuilder<Pocos.TeacherSubject> builder)
        {
            builder.HasKey(x => x.SubjectCode);
        }
    }

    #endregion
}