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
            var uri = string.Format(Endpoint.TEACHER_SUBJECT, "TC-0001");
            return await _requestHandler.GetAsync<List<TeacherSubject>>(uri);
        }

        public async Task<bool> MarkStudentAttendance(string studentNumber, int scheduleId)
        {
            return await _requestHandler.PostAsync<bool, PayloadMarkStudentAttendance>(Endpoint.TEACHER_MARK_STUDENT_ATTENDANCE, new PayloadMarkStudentAttendance
            {
                StudentNumber = studentNumber,
                ScheduleId = scheduleId,
                Date = DateTime.Now.Date
            });
        }

        public async Task<ActiveAttendance> StartAttendanceSession(int ScheduleId)
        {
            var uri = string.Format(Endpoint.TEACHER_ATTENDANCE_START, ScheduleId);
            return await _requestHandler.PostAsync<ActiveAttendance>(uri);
        }

        public async Task<bool> StopAttendanceSession(int AttendanceSession)
        {
            var uri = string.Format(Endpoint.TEACHER_ATTENDANCE_START, AttendanceSession);
            return await _requestHandler.PostAsync<bool>(uri);

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
