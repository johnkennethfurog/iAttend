using System;
using System.Collections.Generic;

namespace IAttend.API.Models
{
    public class Attendance
    {
        public int ID { get; set; }
    
        public Schedule Schedule { get; set; }

        public int ScheduleID { get; set; }
    
        public DateTime Date { get; set; }
    
        public DateTime TimeStarted { get; set; }
    
        public bool IsOpen { get; set; }

        public ICollection<StudentAttendance> StudentAttendances { get; set; }
    }
}