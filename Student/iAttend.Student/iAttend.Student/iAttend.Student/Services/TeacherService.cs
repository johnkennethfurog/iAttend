using iAttend.Student.Helpers;
using iAttend.Student.Interfaces;
using iAttend.Student.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iAttend.Student.Services
{
    class TeacherService : ITeacherService
    {
        private readonly IRequestHandler _requestHandler;

        public TeacherService(IRequestHandler requestHandler)
        {
            _requestHandler = requestHandler;
            _requestHandler.Init(this);
        }

        public async Task<bool> GenerateReport(List<TeacherSubject> subjects, DateTime from, DateTime to)
        {
            var payload = new PayloadReportFilter()
            {
                Subjects = subjects,
                DateFrom = from,
                DateTo = to
            };

            return await _requestHandler.PostAsync<bool, PayloadReportFilter>(Endpoint.TEACHER_REPORT, payload);
        }

        public async Task<List<TeacherStudentAttendance>> GetStudents(int scheduleId, DateTime date)
        {
            var uri = string.Format(Endpoint.TEACHER_STUDENT, scheduleId,date.ToString("MM-dd-yyyy"));
            return await _requestHandler.GetAsync<List<TeacherStudentAttendance>>(uri);
        }

        public async Task<List<TeacherSubject>> GetSubjects(string instructorNumber)
        {
            var uri = string.Format(Endpoint.TEACHER_SUBJECT, instructorNumber);
            return await _requestHandler.GetAsync<List<TeacherSubject>>(uri);
        }

        public async Task<List<TeacherSubject>> GetSubjects()
        {
            return await _requestHandler.GetAsync<List<TeacherSubject>>(Endpoint.TEACHER_SUBJECT);
        }

        public async Task<bool> MarkStudentAttendance(string studentNumber, int scheduleId,DateTime date)
        {
            return await _requestHandler.PostAsync<bool, PayloadMarkStudentAttendance>(Endpoint.TEACHER_MARK_STUDENT_ATTENDANCE, new PayloadMarkStudentAttendance
            {
                StudentNumber = studentNumber,
                ScheduleId = scheduleId,
                Date = date
            });
        }

        public async Task<bool> SignIn(string instructorNumber, string password)
        {
            var payload = new PayloadLogin
            {
                InstructorNumber = instructorNumber,
                Password = password
            };

            var token = await _requestHandler.PostAsync<Token, PayloadLogin>(Endpoint.TEACHER_LOGIN, payload);


            _requestHandler.AddToken(token.TokenString);

            return true;
        }

        public bool SignOut()
        {
            _requestHandler.ClearToken();
            return true;
        }

        public async Task<ActiveAttendance> StartAttendanceSession(int ScheduleId,string room)
        {
            var uri = string.Format(Endpoint.TEACHER_ATTENDANCE_START, room,ScheduleId);
            return await _requestHandler.PostAsync<ActiveAttendance>(uri);
        }

        public async Task<bool> StopAllAttendanceSession(int ScheduleId, string room)
        {
            var uri = string.Format(Endpoint.TEACHER_ATTENDANCES_STOP_ALL, room, ScheduleId);
            return await _requestHandler.PutAsync<bool>(uri);
        }

        public async Task<bool> StopAttendanceSession(int AttendanceSession,string room)
        {
            var uri = string.Format(Endpoint.TEACHER_ATTENDANCES_STOP,room, AttendanceSession);
            return await _requestHandler.PutAsync<bool>(uri);

        }

        public async Task<bool> UnmarkStudentAttendance(string studentNumber, int scheduleId)
        {
            return await _requestHandler.PutAsync<bool, PayloadMarkStudentAttendance>(Endpoint.TEACHER_MARK_STUDENT_ATTENDANCE, new PayloadMarkStudentAttendance
            {
                StudentNumber = studentNumber,
                ScheduleId = scheduleId,
                Date = DateTime.Now.Date
            });

        }
    }
}
