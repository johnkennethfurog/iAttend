using System;

namespace IAttend.API.Models
{
    public class StudentAttendance
    {
        public int ID { get; set; }
    
        public Student Student { get; set; }
    
        public Attendance Attendance { get; set; }

        public int AttendanceID { get; set; }
    
        public DateTime Time { get; set; }
    
        public bool IsScanned { get; set; }
    }
}