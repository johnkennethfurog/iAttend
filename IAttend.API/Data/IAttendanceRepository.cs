using System;
using System.Threading.Tasks;
using IAttend.API.Models;

namespace IAttend.API.Data
{
    public interface IAttendanceRepository
    {
         Task<bool> DoesAttendanceExistAndIsActive(int attendanceId);

         Task<bool> CreateStudentAttendance(int attendanceId,string studentNumber,bool isScanned);

         Task<Attendance> GetAttendance(int attendanceId);
    }
}