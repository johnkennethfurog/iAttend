using System;
using System.Collections.Generic;
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

        //mark students attendance
        public async Task<bool> MarkAtendance(int attendanceId,string studentNumber, bool isScanned)
        {
            var attendance = await GetAttendance(attendanceId);



            var studentAttendance = new StudentAttendance()
            {
                StudentNumber = studentNumber,
                IsScanned = isScanned,
                Attendance = attendance,
                Time = DateTime.Now
            };

            await _dataContext.StudentAttendances.AddAsync(studentAttendance);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }

        //unmark students attendacnce 
        public async Task<bool> UnMarkAtendance(string studentNumber, int scheduleId, DateTime date)
        {
            var attendance = await GetAttendance(scheduleId,date);
            if(attendance == null)
                return false;

            var studentAtendances = await _dataContext.StudentAttendances
                                .Where(x => x.Attendance.ID == attendance.ID && x.StudentNumber == studentNumber ).ToListAsync();

            _dataContext.StudentAttendances.RemoveRange(studentAtendances);

            return await _dataContext.SaveChangesAsync() > 0;
        }

        // check if attendance session already exist or if it is active
        public async Task<bool> DoesAttendanceExistAndIsActive(int attendanceId)
        {
            var attendance = await GetAttendance(attendanceId);

            if(attendance != null)
                return attendance.IsOpen;
            else
                return false;
        }

        //get attendance based on attendanceId
        public async Task<Attendance> GetAttendance(int attendanceId)
        {
            var attendance = await _dataContext.Attendances.FirstOrDefaultAsync(x => x.ID == attendanceId);

            return attendance;
        }

        //get attendance for specific schedule(subject) and date
        public async Task<Attendance> GetAttendance(int scheduleId, DateTime date)
        {
            var attendances = await _dataContext.Attendances.Where(x =>
                                    x.ScheduleID == scheduleId).ToListAsync();

            var attendance = attendances.FirstOrDefault(x => x.Date.Date == date.Date);

            return attendance;
        }

        //get students attendance for specific schedule(subject)
        public async Task<List<Attendance>> GetStudentAttendances(int scheduleId, string studentNumber)
        {
            var attendances = await _dataContext.Attendances.Where(x => x.ScheduleID == scheduleId)
                            .Include(attendance => attendance.StudentAttendances)
                            .ToListAsync();

            attendances.ForEach(attendance =>
            {
                attendance.StudentAttendances = attendance.StudentAttendances.Where(x => x.StudentNumber == studentNumber).ToList();
            });

            return attendances;
        }

        public async Task<Attendance> StartAttendanceSession(int scheduleId)
        {
            var date = DateTime.Now.Date;
            var attendance = await GetAttendance(scheduleId,date);

            if(attendance != null && !attendance.IsOpen)
                attendance.IsOpen = true;
            else if(attendance == null)
            {
                attendance = new Attendance()
                {
                    ScheduleID = scheduleId,
                    Date = date,
                    TimeStarted = DateTime.Now,
                    IsOpen = true
                };
                _dataContext.Attendances.Add(attendance);
            }
            else
            {
                return null;
            }

            await _dataContext.SaveChangesAsync();

            return  attendance;
        }

        public async Task<bool> StopAttendanceSession(int attendacnceId)
        {
            var date = DateTime.Now.Date;
            var attendance = await GetAttendance(attendacnceId);

            if(attendance == null)
                return false;

            attendance.IsOpen = false;

            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<List<StudentAttendance>> GetStudentAttendances(int scheduleId, DateTime date)
        {

            var attendance = GetAttendance(scheduleId,date);

            var attendanceQuery = _dataContext.Attendances.Where(x => x.ID == attendance.Id)
            .Include(x => x.StudentAttendances);

            var studentsAttendance = await attendanceQuery.FirstOrDefaultAsync();
            //  var allSchedulesAttendances = _dataContext.Attendances.Where(x =>
            //                         x.ScheduleID == scheduleId);

            // var specificAttendance = allSchedulesAttendances.Where(x => x.Date.Date == date.Date);

            // var g = specificAttendance.Include(x => x.StudentAttendances);

            // var studentAttendances = await g.FirstOrDefaultAsync();

            // return studentAttendances.StudentAttendances.ToList();
        
            return studentsAttendance.StudentAttendances.ToList();
        }
    }
}