using IAttend.API.Models;
using IAttend.API.Pocos;
using IAttend.API.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API.Data
{
    public class AttendanceRepositoryMSql : IAttendanceRepository
    {
        private readonly DataContext _dataContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICommunication _email;

        public AttendanceRepositoryMSql(
            DataContext dataContext,
            IHostingEnvironment hostingEnvironment,
            ICommunication email)
        {
            _hostingEnvironment = hostingEnvironment;
            _email = email;
            _dataContext = dataContext;
        }

        //mark students attendance
        public async Task<bool> MarkAtendance(int attendanceId, string studentNumber, bool isScanned,string guid)
        {
            var attendance =  await _dataContext.Attendances.Where(x => x.ID == attendanceId).Include(x => x.Schedule).FirstOrDefaultAsync();

            if (attendance == null || (guid != "" && guid != attendance.Guid))
                return false;

            var studentAttendance = new StudentAttendance()
            {
                StudentNumber = studentNumber,
                IsScanned = isScanned,
                Attendance = attendance,
                Time = DateTime.Now,
                Schedule = attendance.Schedule
                
            };

            await _dataContext.StudentAttendances.AddAsync(studentAttendance);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }

        //unmark students attendacnce 
        public async Task<bool> UnMarkAtendance(string studentNumber, int scheduleId, DateTime date)
        {
            var attendance = await GetAttendance(scheduleId, date);
            if (attendance == null)
                return false;

            var studentAtendances = await _dataContext.StudentAttendances
                                .Where(x => x.Attendance.ID == attendance.ID && x.StudentNumber == studentNumber).ToListAsync();

            _dataContext.StudentAttendances.RemoveRange(studentAtendances);

            return await _dataContext.SaveChangesAsync() > 0;
        }

        // check if attendance session already exist or if it is active
        public async Task<bool> DoesAttendanceExistAndIsActive(int attendanceId)
        {
            var attendance = await GetAttendance(attendanceId);

            if (attendance != null)
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

        public async Task<bool> DoesStudentHasAttendance(string studentNumber,int attendanceId)
        {
            var attendance = await _dataContext.Attendances.Where(x => x.ID == attendanceId)
                .AsQueryable().Include(x => x.StudentAttendances).FirstAsync();

            return attendance.StudentAttendances.FirstOrDefault(x => x.StudentNumber == studentNumber) != null ? true : false;
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

        public async Task<Attendance> StartAttendanceSession(int scheduleId, DateTime? sessionDate, bool isOpen = true)
        {
            var date = sessionDate ?? DateTime.Now;
            var attendance = await GetAttendance(scheduleId, date.Date);

            if (attendance != null && !attendance.IsOpen)
            {
                attendance.IsOpen = true;
                attendance.Guid = Guid.NewGuid().ToString();

            }
            else if (attendance == null)
            {
                attendance = new Attendance()
                {
                    ScheduleID = scheduleId,
                    Date = date.Date,
                    TimeStarted = DateTime.Now,
                    IsOpen = isOpen,
                    Guid = Guid.NewGuid().ToString()
            };
                _dataContext.Attendances.Add(attendance);
            }
            else
            {
                return null;
            }

            await _dataContext.SaveChangesAsync();

            return attendance;
        }

        public async Task<bool> StopAttendanceSession(int attendacnceId)
        {
            var date = DateTime.Now.Date;
            var attendance = await GetAttendance(attendacnceId);

            if (attendance == null)
                return false;

            attendance.IsOpen = false;

            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> StopAllAttendanceSession(int scheduleId)
        {
            var attendances = _dataContext.Attendances.Where(x => x.ScheduleID == scheduleId);
            await attendances.ForEachAsync(x => x.IsOpen = false);
            return await _dataContext.SaveChangesAsync() > 0;

        }


        public async Task<List<Pocos.StudentsSubjectAttendance>> GetStudentAttendances(int scheduleId, DateTime? date)
        {
            var dateToFind = date ?? DateTime.Now;
            return await _dataContext.StudentsSubjectAttendances.FromSql("SELECT * FROM dbo.tvfStudentAttendances({0},{1})", scheduleId,dateToFind).ToListAsync();
        }

        public async Task<string> GetSchedulesMasterList(int scheduleId)
        {
            var students = await _dataContext.StudentSubjects
                .Include(x => x.Student)
                .Include(x => x.Schedule)
                .Where(x => x.Schedule.ID == scheduleId)
                .Select(x => x.Student.StudentNumber).ToListAsync();

            return JsonConvert.SerializeObject(students);
        }

        public async Task<Attendance> GetActiveAttendanceSession(string instructorNumber)
        {
            return await _dataContext.Attendances.FromSql("SELECT ATT.* FROM Attendances ATT "+
                        "LEFT JOIN Schedules SCHED ON SCHED.ID = ATT.ScheduleID" +
                        "LEFT JOIN Instructors INS ON INS.InstructorNumber = SCHED.InstructorNumber" +
                        "WHERE INS.InstructorNumber = '{0}' AND IsOpen = 1",instructorNumber).FirstOrDefaultAsync();
        }

        public async Task<bool> GenerateAttendancesReport(string emailAddress,string subjectName, string time,int scheduleId,DateTime from,DateTime to)
        {
            using (var command = _dataContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = Helpers.ScriptHelper.StudentsAttendances(scheduleId,from,to);
                await _dataContext.Database.OpenConnectionAsync();
                var result =  await command.ExecuteReaderAsync();

                string name = $"{subjectName}_{time}_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}";
                name = name.Replace(":", "");
                name = name.Replace(" ","");
                int rowInd = 0;

                var excel = new ExcelReporter(_hostingEnvironment.WebRootPath, $"{name}.xlsx");
                var sheet = excel.CreateSheet("Test");

                var headerRow = sheet.CreateRow(rowInd);
                for (int i = 0; i < result.FieldCount; i++)
                {
                    headerRow.CreateCell(i).SetCellValue(result.GetName(i));

                }

                rowInd++;

                while (result.Read())
                {
                    var row = sheet.CreateRow(rowInd);
                    row.CreateCell(0).SetCellValue(result.GetString(0));
                    row.CreateCell(1).SetCellValue(result.GetString(1));

                    if(result.FieldCount > 2)
                    {
                        for (int i = 2; i < result.FieldCount; i++)
                        {
                            row.CreateCell(i).SetCellValue(result.GetInt32(i));
                        }
                    }

                    rowInd++;
                }


                excel.WriteFileStream();
                await _email.SendEmail(excel.ExcelFilePath, emailAddress, subjectName, "Kindly See Attached File");

                excel.Dispose();

                _dataContext.Database.CloseConnection();
                result.Close();



            }

            return true;
        }

        public async Task<List<StudentsAbsentStat>> GetStudentsAbsent(string instructorNumber, int absentCount)
        {
            return await _dataContext.StudentsAttendanceStats.FromSql("select * from tvf_StudentAttendancesStat({0},{1})", absentCount, instructorNumber).ToListAsync();
        }
    }
}
