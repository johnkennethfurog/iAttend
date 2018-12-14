using System;

namespace IAttend.API.Dtos
{
    public class StudentToAttendanceDto
    {
        public int AttendanceSessionId { get; set; }
    
        public string StudentNumber { get; set; }

        public string Guid { get; set; }

        public string Time { get; set; }

        public string Subject { get; set; }

        public string StudentName { get; set; }
    }
}