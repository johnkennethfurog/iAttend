using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IAttend.API.Models;

namespace IAttend.API.Data
{
    public interface IAttendanceRepository
    {
         Task<bool> DoesAttendanceExistAndIsActive(int attendanceId);

         Task<bool> MarkAtendance(int attendanceId,string studentNumber,bool isScanned);

        Task<bool> UnMarkAtendance(string studentNumber,int scheduleId, DateTime date);

         Task<Attendance> GetAttendance(int attendanceId);

        Task<Attendance> GetAttendance(int scheduleId,DateTime date);

         Task<List<Attendance>> GetStudentAttendances(int scheduleId, string studentNumber);

         Task<List<StudentAttendance>> GetStudentAttendances(int scheduleId, DateTime date);

         Task<Attendance> StartAttendanceSession(int scheduleId);

         Task<bool> StopAttendanceSession(int attendacnceId);
    }
}