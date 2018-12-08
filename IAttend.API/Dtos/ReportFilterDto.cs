using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API.Dtos
{
    public class ReportFilterDto
    {
        public List<TeacherSubjectDto> Subjects { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

    }
}
