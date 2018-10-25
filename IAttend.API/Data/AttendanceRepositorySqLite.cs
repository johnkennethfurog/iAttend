using System;
using System.Linq;
using System.Threading.Tasks;
using IAttend.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IAttend.API.Data
{
    public class AttendanceRepositorySqLite : IAttendanceRepository
    {
        private readonly DataContext _dataContext;

        public AttendanceRepositorySqLite(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> CreateStudentAttendance(int attendanceId,string studentNumber, bool isScanned)
        {
            var attendance = await GetAttendance(attendanceId);
            var serverDate = DateTime.Now;


            var studentAttendance = new StudentAttendance()
            {
                StudentNumber = studentNumber,
                IsScanned = isScanned,
                Attendance = attendance,
            };

            await _dataContext.StudentAttendances.AddAsync(studentAttendance);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DoesAttendanceExistAndIsActive(int attendanceId)
        {
            var attendance = await GetAttendance(attendanceId);

            if(attendance != null)
                return attendance.IsOpen;
            else
                return false;
        }

        public async Task<Attendance> GetAttendance(int attendanceId)
        {
            var attendance = await _dataContext.Attendances.FirstOrDefaultAsync(x => x.ID == attendanceId);

            return attendance;
        }
    }
}