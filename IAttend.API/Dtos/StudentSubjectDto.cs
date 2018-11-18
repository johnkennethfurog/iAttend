namespace IAttend.API.Dtos
{
    public class StudentSubjectDto
    {
        public string StudentNumber { get; set; }
        public int ScheduleID { get; set; }
        public string InstructorNumber{ get;set;}
        public string Instructor { get; set; }
    
        public string Avatar { get; set; }

        public string Room { get; set; }

        public string Time { get; set; }

        public string DayOfWeek { get; set; }

        public string SubjectCode { get; set; }
        
        public string Subject { get; set; }

    }
}