using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API.Pocos
{
    public class StudentsSubjectAttendance
    {
        public string StudentNumber { get; set; }
        public string StudentName { get; set; }
        public string Avatar { get; set; }
        public bool? IsScanned { get; set; }
    }
}
