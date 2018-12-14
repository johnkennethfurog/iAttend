using System;

namespace IAttend.API.Dtos
{
    public class TeacherToStudentAttendanceDto
    {
        public DateTime Date { get; set; }
    
        public string StudentNumber { get; set; }
    
        public int ScheduleId { get; set; }

        public string Time { get; set; }

        public string Subject { get; set; }

        public string StudentName { get; set; }
    }
}