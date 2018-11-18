using System;
using System.ComponentModel.DataAnnotations;

namespace IAttend.API.Pocos
{
    public class StudentSubject
    {
        public int ScheduleID { get; set; }
        public string StudentNumber { get; set; }
        public string Instructor { get; set; }    
        public string InstructorNumber{ get;set;}
        public string Avatar { get; set; }

        public string Room { get; set; }

        public DateTime Time { get; set; }

        public int DayOfWeek { get; set; }

        public string SubjectCode { get; set; }
        
        public string Subject { get; set; }

    }
}