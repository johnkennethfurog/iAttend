using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API.Dtos
{
    public class TeacherFetchStudentDto
    {
        public int ScheduleId { get; set; }
        public DateTime Date { get; set; }
    }
}
