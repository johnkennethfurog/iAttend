using System;

namespace IAttend.API.Dtos
{
    public class TeacherSubjectDto
    {
        public int SchedID { get; set; }
        
        public string Room { get; set; }

        public string Time { get; set; }

        public string DayOfWeek { get; set; }

        public string Code {get;set;}

        public string Name { get; set; }

        public int StudentCount { get; set; }
    }
}