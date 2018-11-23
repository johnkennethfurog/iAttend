using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API.Pocos
{
    public class Schedule
    {
        public string Instructor { get; set; }

        public string Avatar { get; set; }

        public DateTime Time { get; set; }

        public string SubjectCode { get; set; }

        public string Subject { get; set; }

        public int ScheduleID { get; set; }

        public string Room { get; set; }

        public int DayOfWeek { get; set; }
    }
}
