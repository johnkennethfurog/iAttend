using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API.Dtos
{
    public class AddScheduleDto
    {
        public string SubjectCode { get; set; }

        public string Room { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public int DayOfWeek { get; set; }
    }
}
