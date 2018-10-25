using System;

namespace IAttend.API.Dtos
{
    public class StudentToAttendanceDto
    {
        public int AttendanceSessionId { get; set; }
    
        public string StudentNumber { get; set; }
    
        public DateTime Time { get; set; }

        public bool IsScanned { get; set; }
        }
}