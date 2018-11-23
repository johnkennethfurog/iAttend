using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API.Dtos
{
    public class ScheduleDto
    {
        public int ScheduleID { get; set; }

        public string Instructor { get; set; }

        public string Avatar { get; set; }

        public string Room { get; set; }

        public string Time { get; set; }

        public string SubjectCode { get; set; }

        public string Subject { get; set; }
    }
}
