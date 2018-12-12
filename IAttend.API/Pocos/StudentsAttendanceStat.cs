using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API.Pocos
{
    public class StudentsAbsentStat
    {
        public string StudentNumber { get; set; }
        public string StudentName { get; set; }
        public string Avatar { get; set; }
        public string SubjectCode { get; set; }
        public string Subject { get; set; }
        public string Room { get; set; }
        public string Time { get; set; }
        public int Present { get; set; }
        public int Absent { get; set; }
        public int TotalAttendance { get; set; }
        public string InstructorNumber { get; set; }
    }
}
