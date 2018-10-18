using System;

namespace IAttend.API.Dtos
{
    public class StudentAttendanceDto
    {
        public DateTime Date { get; set; }
    
        public bool IsPresent { get; set; }
    }
}