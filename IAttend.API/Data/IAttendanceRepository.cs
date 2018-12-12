using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IAttend.API.Models;

namespace IAttend.API.Data
{
    public interface IAttendanceRepository
    {
        Task<string> GetSchedulesMasterList(int scheduleId);

         Task<bool> DoesAttendanceExistAndIsActive(int attendanceId);

        Task<bool> DoesStudentHasAttendance(string studentNumber, int attendanceId);

         Task<bool> MarkAtendance(int attendanceId,string studentNumber,bool isScanned);

        Task<bool> UnMarkAtendance(string studentNumber,int scheduleId, DateTime date);

         Task<Attendance> GetAttendance(int attendanceId);

        Task<Attendance> GetAttendance(int scheduleId,DateTime date);

         Task<List<Attendance>> GetStudentAttendances(int scheduleId, string studentNumber);

         Task<List<Pocos.StudentsSubjectAttendance>> GetStudentAttendances(int scheduleId, DateTime? date);

        [Obsolete]
        Task<Attendance> GetActiveAttendanceSession(string instructorNumber);

        Task<Attendance> StartAttendanceSession(int scheduleId, DateTime? sessionDate, bool isOpen = true);

        Task<bool> StopAttendanceSession(int attendacnceId);
        Task<bool> StopAllAttendanceSession(int scheduleId);

        Task<bool> GenerateAttendancesReport(string emailAddress,string subjectName,string time,int scheduleId, DateTime from, DateTime to);
        Task<List<Pocos.StudentsAbsentStat>> GetStudentsAbsent(string instructorNumber, int absentCount);

    }
}