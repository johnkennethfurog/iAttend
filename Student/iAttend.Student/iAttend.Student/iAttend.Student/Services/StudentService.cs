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

        public StudentService(IRequestHandler requestHandler)
        {
            _requestHandler = requestHandler;
            _requestHandler.Init(this);
        }

        public async Task<List<StudentAttendance>> GetAttendances(string studentNumber, int scheduleId)
        {
            //MOCK DATA
            var json = "[{\"date\":\"2018 - 10 - 24T00: 00:00\",\"isPresent\":true},{\"date\":\"2018 - 10 - 25T00: 00:00\",\"isPresent\":false},{\"date\":\"2018 - 10 - 26T00: 00:00\",\"isPresent\":false},{\"date\":\"2018 - 10 - 27T00: 00:00\",\"isPresent\":false},{\"date\":\"2018 - 10 - 28T00: 00:00\",\"isPresent\":true},{\"date\":\"2018 - 10 - 29T00: 00:00\",\"isPresent\":false}]";
            return JsonConvert.DeserializeObject<List<StudentAttendance>>(json);

            var uriRequest = string.Format(Endpoint.STUDENT_ATTENDANCE, studentNumber, scheduleId);
            return await _requestHandler.GetAsync<List<StudentAttendance>>(uriRequest);
        }

        public async Task<List<StudentSubject>> GetSubjects(string studentNumber)
        {
            //MOCK DATA
            var json = "[{\"instructor\":{\"id\":1,\"name\":\"Velazquez Caldwell\",\"avatar\":\"https://randomuser.me/api/portraits/men/1.jpg\"},\"subject\":{\"id\":1,\"room\":\"LAB 1\",\"time\":\"10:49\",\"dayOfWeek\":\"Saturday\",\"name\":\"Computer Programming 1\"}},{\"instructor\":{\"id\":1,\"name\":\"Velazquez Caldwell\",\"avatar\":\"https://randomuser.me/api/portraits/men/1.jpg\"},\"subject\":{\"id\":2,\"room\":\"LAB 1\",\"time\":\"00:40\",\"dayOfWeek\":\"Monday\",\"name\":\"Computer Programming 2\"}},{\"instructor\":{\"id\":1,\"name\":\"Velazquez Caldwell\",\"avatar\":\"https://randomuser.me/api/portraits/men/1.jpg\"},\"subject\":{\"id\":3,\"room\":\"LAB 1\",\"time\":\"04:42\",\"dayOfWeek\":\"Monday\",\"name\":\"Object Oriented Programming Languages\"}},{\"instructor\":{\"id\":1,\"name\":\"Velazquez Caldwell\",\"avatar\":\"https://randomuser.me/api/portraits/men/1.jpg\"},\"subject\":{\"id\":4,\"room\":\"LAB 2\",\"time\":\"03:10\",\"dayOfWeek\":\"Tuesday\",\"name\":\"Databese Management 2\"}},{\"instructor\":{\"id\":1,\"name\":\"Velazquez Caldwell\",\"avatar\":\"https://randomuser.me/api/portraits/men/1.jpg\"},\"subject\":{\"id\":5,\"room\":\"LAB 2\",\"time\":\"07:14\",\"dayOfWeek\":\"Thursday\",\"name\":\"Database Management 1\"}},{\"instructor\":{\"id\":2,\"name\":\"Trujillo Gilliam\",\"avatar\":\"https://randomuser.me/api/portraits/men/12.jpg\"},\"subject\":{\"id\":6,\"room\":\"LAB 3\",\"time\":\"07:00\",\"dayOfWeek\":\"Tuesday\",\"name\":\"Computer Programming 1\"}},{\"instructor\":{\"id\":2,\"name\":\"Trujillo Gilliam\",\"avatar\":\"https://randomuser.me/api/portraits/men/12.jpg\"},\"subject\":{\"id\":7,\"room\":\"LAB 2\",\"time\":\"01:41\",\"dayOfWeek\":\"Wendesday\",\"name\":\"Computer Programming 2\"}},{\"instructor\":{\"id\":2,\"name\":\"Trujillo Gilliam\",\"avatar\":\"https://randomuser.me/api/portraits/men/12.jpg\"},\"subject\":{\"id\":8,\"room\":\"LAB 1\",\"time\":\"13:55\",\"dayOfWeek\":\"Thursday\",\"name\":\"Object Oriented Programming Languages\"}},{\"instructor\":{\"id\":2,\"name\":\"Trujillo Gilliam\",\"avatar\":\"https://randomuser.me/api/portraits/men/12.jpg\"},\"subject\":{\"id\":9,\"room\":\"LAB 3\",\"time\":\"22:36\",\"dayOfWeek\":\"Thursday\",\"name\":\"Databese Management 2\"}},{\"instructor\":{\"id\":2,\"name\":\"Trujillo Gilliam\",\"avatar\":\"https://randomuser.me/api/portraits/men/12.jpg\"},\"subject\":{\"id\":10,\"room\":\"LAB 1\",\"time\":\"22:28\",\"dayOfWeek\":\"Saturday\",\"name\":\"Database Management 1\"}}]";
            return  JsonConvert.DeserializeObject<List<StudentSubject>>(json);


            var uriRequest = string.Format(Endpoint.STUDENT_SUBJECT,studentNumber);
            return await _requestHandler.GetAsync<List<StudentSubject>>(uriRequest); 
        }

        public async Task<bool> ScanDocument(PayloadStudentAttendance attenadancQr)
        {
            //MOCK
            return true;

            return await _requestHandler.PostAsync<bool, PayloadStudentAttendance>(Endpoint.STUDENT_MARK_ATTENDANCE, attenadancQr);
        }
    }
}
