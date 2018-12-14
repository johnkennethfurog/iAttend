using iAttend.Student.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iAttend.Student.Interfaces
{
    public interface ITeacherService
    {
        Task<List<TeacherSubject>> GetSubjects(string instructorNumber);
        Task<List<TeacherSubject>> GetSubjects();

        Task<List<TeacherStudentAttendance>> GetStudents(int scheduleId,DateTime date);
        Task<bool> MarkStudentAttendance(string studentNumber, int scheduleId,DateTime date,string subject , string studentName,string time);
        Task<bool> UnmarkStudentAttendance(string studentNumber, int scheduleId);

        Task<ActiveAttendance> StartAttendanceSession(int ScheduleId,string room);
        Task<bool> StopAttendanceSession(int AttendanceSession,string room);
        Task<bool> StopAllAttendanceSession(int ScheduleId, string room);

        Task<bool> SignIn(string instructorNumber, string password);

        Task<bool> GenerateReport(List<TeacherSubject> subjects, DateTime from,DateTime to);

        Task<AbsentStat> GetAbsentStat();

        bool SignOut();
    }
}
