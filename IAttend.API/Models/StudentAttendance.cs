using System;

namespace IAttend.API.Models
{
    public class StudentAttendance
    {
        public int ID { get; set; }
    
        public string StudentNumber { get; set; }
    
        public Attendance Attendance { get; set; }
    
        public DateTime Time { get; set; }
    
        public bool IsScanned { get; set; }

        public Schedule Schedule { get; set; }
    }
}