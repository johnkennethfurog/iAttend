using System;
using System.Collections.Generic;

namespace IAttend.API.Models
{
    public class Schedule
    {
        public int ID { get; set; }
    
        public Subject Subject { get; set; }

        public Instructor Instructor { get; set; }

        public string Room { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public int DayOfWeek { get; set; }

        public ICollection<StudentSubject> StudentSubjects { get; set; }

        public ICollection<StudentAttendance> StudentAttendances { get; set; }
    }
}