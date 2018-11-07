using System;

namespace IAttend.API.Dtos
{
    public class TeacherToStudentAttendanceDto
    {
        public DateTime Date { get; set; }
    
        public string StudentNumber { get; set; }
    
        public int ScheduleId { get; set; }
    }
}