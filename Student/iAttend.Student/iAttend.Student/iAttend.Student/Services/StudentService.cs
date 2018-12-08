using iAttend.Student.Helpers;
using iAttend.Student.Interfaces;
using iAttend.Student.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iAttend.Student.Services
{
    class StudentService : IStudentService
    {
        private readonly IRequestHandler _requestHandler;

        public StudentService(
            IRequestHandler requestHandler,
            IPreferences preferences)
        {
            _requestHandler = requestHandler;
            _requestHandler.Init(this);
        }

        public async Task<List<StudentAttendance>> GetAttendances(string studentNumber, int scheduleId)
        {

            var uriRequest = string.Format(Endpoint.STUDENT_ATTENDANCE, studentNumber, scheduleId);
            return await _requestHandler.GetAsync<List<StudentAttendance>>(uriRequest);
        }

        public async Task<StudentInfo> GetStudent(string studentNumber)
        {
            var uri = string.Format(Endpoint.STUDENT_CONFIRM,studentNumber);
            return await _requestHandler.GetAsync<StudentInfo>(uri);
        }

        public async Task<List<StudentSubject>> GetSubjects(string studentNumber)
        {

            var uriRequest = string.Format(Endpoint.STUDENT_SUBJECT,studentNumber);
            return await _requestHandler.GetAsync<List<StudentSubject>>(uriRequest); 
        }

        public async Task<bool> ScanDocument(PayloadStudentAttendance attenadancQr)
        {
            return await _requestHandler.PostAsync<bool, PayloadStudentAttendance>(Endpoint.STUDENT_MARK_ATTENDANCE, attenadancQr);
        }
    }
}
