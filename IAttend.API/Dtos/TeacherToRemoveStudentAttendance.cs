using System;

namespace IAttend.API.Dtos
{
    public class TeacherToRemoveStudentAttendance
    {
        public int ScheduleId { get; set; }
    
        public string StudentNumber { get; set; }
    
        public DateTime Date { get; set; }
    }
}